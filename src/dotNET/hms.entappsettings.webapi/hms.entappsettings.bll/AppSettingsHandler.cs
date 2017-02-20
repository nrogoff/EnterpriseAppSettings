// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 02
// 
// Project: hms.entappsettings.bll
// Filename: AppSettingsHandler.cs

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using hms.entappsettings.contracts;
using hms.entappsettings.model;
using hms.entappsettings.repository.Repositories;

namespace hms.entappsettings.bll
{
    /// <summary>
    /// Handler for AppSetting and AppSettingDTO
    /// </summary>
    public class AppSettingsHandler : IAppSettingHandler
    {
        public int PlatformTenantId { get; set; }

        private readonly IAppSettingRepository _appSettingRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IAppSettingGroupRepository _appSettingGroupRepository;
        private readonly IMapper _mapper;

        public AppSettingsHandler(IAppSettingRepository appSettingRepository, ITenantRepository tenantRepository, 
            IAppSettingGroupRepository appSettingGroupRepository, IMapper mapper)
        {
            PlatformTenantId = 1;
            _appSettingRepository = appSettingRepository;
            _tenantRepository = tenantRepository;
            _appSettingGroupRepository = appSettingGroupRepository;
            _mapper = mapper;
        }


        /// <summary>
        /// Returns the resultant AppSettings for a particular affiliate, including unaffiliated (=0) and including all Groups that apply.
        /// Also includes all settings that have been overridden. These are flagged as overridden.
        /// </summary>
        /// <param name="tenantId">The Id of the tenant. When used from management UI then can leave as null to get all Tenants</param>
        /// <param name="appSettingGroupId"></param>
        /// <param name="includeInternal">Include internal App Settings. Default = false</param>
        /// <returns>Collection of AppSettingsWithOverride.</returns>
        public IEnumerable<AppSettingWithOverrideDTO> GetAppSettingWithOverrideDtos(int tenantId, int appSettingGroupId, bool includeInternal = false)
        {
            if (_tenantRepository.GetById(tenantId) == null)
            {
                throw new ArgumentOutOfRangeException(nameof(tenantId), "Tenant does not exist in the database.");
            }
            if (_appSettingGroupRepository.GetById(appSettingGroupId) == null)
            {
                throw new ArgumentOutOfRangeException(nameof(appSettingGroupId), "App Setting Group does not exist in the database.");
            }

            var allTenantsAppSettings = _appSettingRepository.GetAllAppSettings(includeInternal)
                .Where(s => s.TenantId == tenantId || s.TenantId == PlatformTenantId).ToList();

            //filter to only app settings in passed group and parent groups
            var allAffiliateSettingsInGroup = (from setting in allTenantsAppSettings
                                               join appGroup in _appSettingGroupRepository.GetAppSettingGroupParents(appSettingGroupId)
                                                   on setting.SettingGroupId equals appGroup.Id
                                               orderby setting.AppSettingGroup.GroupPath descending , setting.SettingKey ascending 
                                               select setting).ToList();
            //Map to override DTO
            var appSettingsWithOverride = _mapper.Map<List<AppSettingWithOverrideDTO>>(allAffiliateSettingsInGroup);

            //Log active AppSetting Ids
            var activeAppSettingIds = new HashSet<string>();

            for (int i = 0; i < appSettingsWithOverride.Count; i++)
            {
                if (activeAppSettingIds.Contains(appSettingsWithOverride[i].SettingKey))
                {
                    //Is overriding previous setting
                    appSettingsWithOverride[i].Overridden = true;
                }
                else
                {
                    //Add setting
                    activeAppSettingIds.Add(appSettingsWithOverride[i].SettingKey);
                }
            }

            return appSettingsWithOverride;
        }

        #region IDisposable

        private bool _disposed = false;

        //Finalize method for the object, will call Dispose for us
        //to clean up the resources if the user has not called it
        ~AppSettingsHandler()
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
                    _appSettingRepository?.Dispose();
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