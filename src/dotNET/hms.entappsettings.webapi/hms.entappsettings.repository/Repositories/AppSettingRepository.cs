// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.repository
// Filename: AppSettingRepository.cs

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using hms.entappsettings.context;
using hms.entappsettings.contracts;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    public class AppSettingRepository:EditableRepositoryBase<AppSetting>, IAppSettingRepository
    {
        /// <summary>
        /// The Id of the Platform Tenant (default = 1). Only here in case someone really wants it to be zero and have created it that way in the database.
        /// </summary>
        public int PlatformTenantId { get; set; }

        private readonly IAppSettingGroupRepository _appSettingGroupRepository;
        private readonly ITenantRepository _tenantRepository;

        public AppSettingRepository(IEntAppSettingsDbContext dbContext, IAppSettingGroupRepository appSettingGroupRepository, ITenantRepository tenantRepository) : base(dbContext)
        {
            PlatformTenantId = 1;
            _appSettingGroupRepository = appSettingGroupRepository;
            _tenantRepository = tenantRepository;
        }

        /// <summary>
        /// Returns all AppSettings
        /// </summary>
        /// <param name="includeInternal">Include internal App Settings. Default = false</param>
        /// <returns></returns>
        public IQueryable<AppSetting> GetAllAppSettings(bool includeInternal = false)
        {
            var result = from setting in _dbContext.AppSettings.Include(s => s.AppSettingGroup)
                         orderby setting.AppSettingGroup.GroupPath, setting.SettingKey
                         select setting;

            if (!includeInternal)
            {
                return result.Where(s => s.IsInternalOnly != true);
            }
            return result;
        }

        /// <summary>
        /// Returns the resultant AppSettings for a particular affiliate, including unaffiliated (=0) and including all Groups that apply
        /// </summary>
        /// <param name="tenantId">The Id of the tenant.</param>
        /// <param name="appSettingGroupId"></param>
        /// <param name="includeInternal">Include internal App Settings. Default = false</param>
        /// <returns>Collection of AppSettings. Null if Tenant Id does not exist</returns>
        public IEnumerable<AppSetting> GetResultantAppSettings(int tenantId, int appSettingGroupId, bool includeInternal = false)
        {
            if (_tenantRepository.GetById(tenantId) == null)
            {
                throw new ArgumentOutOfRangeException(nameof(tenantId), "Tenant does not exist in the database.");
            }
            if (_appSettingGroupRepository.GetById(appSettingGroupId) == null)
            {
                throw new ArgumentOutOfRangeException(nameof(appSettingGroupId), "App Setting Group does not exist in the database.");
            }

            var resultantSettings = new List<AppSetting>();
            
            var allTenantsAppSettings = GetAllAppSettings(includeInternal).Where(s => s.TenantId == tenantId || s.TenantId == PlatformTenantId).ToList();

            //filter to only app settings in passed group and parent groups
            var allAffiliateSettingsInGroup = (from setting in allTenantsAppSettings
                                               join appGroup in _appSettingGroupRepository.GetAppSettingGroupParents(appSettingGroupId)
                                                   on setting.SettingGroupId equals appGroup.Id
                                               orderby setting.AppSettingGroup.GroupPath, setting.SettingKey
                                               select setting).ToList();

            Dictionary<string, string> settingAuditDictionary = new Dictionary<string, string>();

            // Build resultant set containing only resultants incl. overrides where applicable
            foreach (var appSetting in allAffiliateSettingsInGroup)
            {
                if (settingAuditDictionary.ContainsKey(appSetting.SettingKey))
                {
                    //Replace setting
                    resultantSettings.Remove(resultantSettings.Find(s => s.SettingKey == appSetting.SettingKey));
                    resultantSettings.Add(appSetting);
                    settingAuditDictionary[appSetting.SettingKey] = appSetting.AppSettingGroup.GroupPath;
                }
                else
                {
                    //Add setting
                    resultantSettings.Add(appSetting);
                    settingAuditDictionary.Add(appSetting.SettingKey, appSetting.AppSettingGroup.GroupPath);
                }
            }

            return resultantSettings;
        }

        /// <summary>
        /// Add an AppSetting
        /// </summary>
        /// <param name="appSetting"></param>
        public void Add(AppSetting appSetting)
        {
            if (appSetting.SettingKey.Contains(" "))
            {
                throw new ArgumentOutOfRangeException(nameof(appSetting),"The App Setting Key cannot contain spaces");
            }
            _dbContext.AppSettings.Add(appSetting);
        }

        /// <summary>
        /// Update an existing App Setting
        /// </summary>
        /// <param name="appSetting"></param>
        public override void Update(AppSetting appSetting)
        {
            var entry = _dbContext.Entry(appSetting);
            if (entry.State == EntityState.Detached)
            {
                //First query the context
                var existingAppSetting = _dbContext.AppSettings.Find(appSetting.AppSettingId);
                if (existingAppSetting != null)
                {
                    //Entity already in context
                    var attachedEntry = _dbContext.Entry(existingAppSetting);
                    attachedEntry.CurrentValues.SetValues(appSetting);
                }
            }
            else
            {
                base.Update(appSetting);
            }
        }

        #region private methods

        /// <summary>
        /// Returns the settings for the Parent Group
        /// </summary>
        /// <param name="allAppSettings">Collection of all app settings for this Tenant and Group</param>
        /// <param name="appSetting"></param>
        /// <param name="groupParents">Parent Groups</param>
        /// <param name="selectedGroupId"></param>
        /// <param name="parentSetting"></param>
        /// <returns></returns>
        private bool TryGetSettingForParentGroup(List<AppSetting> allAppSettings, AppSetting appSetting, List<GetParentAppSettingGroupsReturnModel> groupParents, 
            int selectedGroupId, out AppSetting parentSetting)
        {
            parentSetting = null;

            var parent = groupParents.FirstOrDefault(p => p.Id == selectedGroupId);
            if (parent == null)
                return false;

            parentSetting = allAppSettings.FirstOrDefault(s => s.TenantId == appSetting.TenantId && s.SettingKey == appSetting.SettingKey && s.AppSettingGroup.AppSettingGroupId == parent.ParentGroupId);
            if (parentSetting != null)
                return true;

            if (parent.ParentGroupId != null && TryGetSettingForParentGroup(allAppSettings, appSetting, groupParents, parent.ParentGroupId.Value, out parentSetting))
                return true;

            return false;
        }


        /// <summary>
        /// Enumerates through the list of settings setting the flag as overriden if so.
        /// </summary>
        /// <param name="appSettingList"></param>
        /// <returns></returns>
        protected List<AppSettingWithOverrideDTO> ResolveOverrides(List<AppSettingWithOverrideDTO> appSettingList)
        {
            var resultantSettings = new List<AppSettingWithOverrideDTO>();
            // Ensure the sorting is correct first
            var orderedAppSettingList = (from appSetting in appSettingList
                                         orderby appSetting.GroupPath, appSetting.SettingKey, appSetting.TenantId
                                         select appSetting).ToList();

            Dictionary<string, string> settingAuditDictionary = new Dictionary<string, string>();

            foreach (var appSetting in orderedAppSettingList)
            {
                if (settingAuditDictionary.ContainsKey(appSetting.SettingKey))
                {
                    //Flag setting
                    resultantSettings.FindAll(s => s.SettingKey == appSetting.SettingKey).ForEach(s => s.Overridden = true);
                    resultantSettings.Add(appSetting);
                    settingAuditDictionary[appSetting.SettingKey] = appSetting.GroupPath;
                }
                else
                {
                    //Add setting
                    resultantSettings.Add(appSetting);
                    settingAuditDictionary.Add(appSetting.SettingKey, appSetting.GroupPath);
                }
            }

            return resultantSettings;
        }

        /// <summary>
        /// Removes overridden settings from collection
        /// </summary>
        /// <param name="appSettingList"></param>
        /// <returns></returns>
        protected List<AppSettingWithOverrideDTO> ResolveResultant(List<AppSettingWithOverrideDTO> appSettingList)
        {
            var overridenList = ResolveOverrides(appSettingList);
            overridenList.RemoveAll(s => s.Overridden);

            return overridenList;
        }

        #endregion


        #region IDisposable

        private bool _disposed = false;

        //Finalize method for the object, will call Dispose for us
        //to clean up the resources if the user has not called it
        ~AppSettingRepository()
        {
            //Indicate that the GC called Dispose, not the user
            Dispose(false);
        }

        //This is the public method, it will HOPEFULLY but
        //not always be called by users of the class
        public void Dispose()
        {
            //indicate this was NOT called by the Garbage collector
            Dispose(true);

            //Now we have disposed of all our resources, the GC does not
            //need to do anything, stop the finalizer being called
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            //Check to see if we have already disposed the object
            //this is necessary because we should be able to call
            //Dispose multiple times without throwing an error
            if (!_disposed)
            {
                if (disposing)
                {
                    //clean up managed resources
                    _appSettingGroupRepository?.Dispose();
                    _tenantRepository?.Dispose();
                }

                //clear up any unmanaged resources - this is safe to
                //put outside the disposing check because if the user
                //called dispose we want to also clean up unmanaged
                //resources, if the GC called Dispose then we only
                //want to clean up managed resources
            }
        }

        #endregion

    }
}