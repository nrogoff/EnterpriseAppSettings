// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.repository
// Filename: AppSettingGroupRepository.cs

using System;
using System.Collections.Generic;
using System.Linq;
using hms.entappsettings.context;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    /// <summary>
    /// App Setting Groups Repository
    /// </summary>
    public class AppSettingGroupRepository : EditableRepositoryBase<AppSettingGroup>, IAppSettingGroupRepository
    {

        public AppSettingGroupRepository(IEntAppSettingsDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get the App Setting Group Parents
        /// </summary>
        /// <param name="appSettingGroupId"></param>
        /// <returns></returns>
        public IQueryable<GetParentAppSettingGroupsReturnModel> GetAppSettingGroupParents(int appSettingGroupId)
        {
            return _dbContext.GetParentAppSettingGroups(appSettingGroupId);
        }

        /// <summary>
        /// Add an AppSettingGroup
        /// </summary>
        /// <param name="appSettingGroup"></param>
        public void Add(AppSettingGroup appSettingGroup)
        {
            if (appSettingGroup.ParentGroupId == null)
            {
                throw new ArgumentOutOfRangeException(nameof(appSettingGroup),"Parent Group Id can't be null");
            }
            if (!Exists(appSettingGroup.ParentGroupId.Value))
            {
                throw new ArgumentOutOfRangeException(nameof(appSettingGroup),"Invalid Parent Group Id");
            }

            _dbContext.AppSettingGroups.Add(appSettingGroup);
        }
      

        #region IDisposable

        private bool _disposed = false;

        //Finalize method for the object, will call Dispose for us
        //to clean up the resources if the user has not called it

        ~AppSettingGroupRepository()
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