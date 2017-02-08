// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 02
// 
// Project: hms.entappsettings.dbContext
// Filename: UnitOfWork.cs

using System;
using System.Data.Entity;
using hms.entappsettings.context;
using hms.entappsettings.repository.Repositories;

namespace hms.entappsettings.repository
{
    /// <summary>
    /// Work in progress. Deciding whether it's worth using a Unit Of Work pattern!
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EntAppSettingsDbContext _dbContext;
        private AppSettingRepository _appSettingRepository;
        private AppSettingGroupRepository _appSettingGroupRepository;

        public AppSettingRepository AppSettingRepository
        {
            get { return _appSettingRepository ?? (_appSettingRepository = new AppSettingRepository(_dbContext, AppSettingGroupRepository, TenantRepository)); }
        }

        public AppSettingGroupRepository AppSettingGroupRepository
        {
            get { return _appSettingGroupRepository ?? (_appSettingGroupRepository = new AppSettingGroupRepository(_dbContext)); }
        }

        public TenantRepository TenantRepository
        {
            get { return _tenantRepository ?? (_tenantRepository = new TenantRepository(_dbContext)); ; }
        }

        public UnitOfWork(EntAppSettingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public void SaveAsync()
        {
            _dbContext.SaveChangesAsync();
        }


        #region IDisposable

        private bool _disposed = false;
        private TenantRepository _tenantRepository;

        //Finalize method for the object, will call Dispose for us
        //to clean up the resources if the user has not called it
        ~UnitOfWork()
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
                    _dbContext?.Dispose();
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

    public interface IUnitOfWork : IDisposable
    {
    }
}