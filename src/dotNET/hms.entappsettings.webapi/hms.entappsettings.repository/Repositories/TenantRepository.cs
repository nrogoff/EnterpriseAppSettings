// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 08
// 
// Project: hms.entappsettings.repository
// Filename: TenantRepository.cs

using System;
using System.Data.Entity;
using hms.entappsettings.context;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    public class TenantRepository : EditableRepositoryBase<Tenant>, ITenantRepository
    {

        public TenantRepository(IEntAppSettingsDbContext dbContext) : base(dbContext)
        {
        }

        public void Add(Tenant tenant)
        {
            _dbContext.Tenants.Add(tenant);
        }

        public override void Update(Tenant tenant)
        {

            var entry = _dbContext.Entry(tenant);
            if (entry.State == EntityState.Detached)
            {
                //First query the context
                var existingTenant = _dbContext.Tenants.Find(tenant.TenantId);
                if (existingTenant != null)
                {
                    //Entity already in context
                    var attachedEntry = _dbContext.Entry(existingTenant);
                    attachedEntry.CurrentValues.SetValues(tenant);
                }
            }
            else
            {
                base.Update(tenant);               
            }
        }

        #region IDisposable

        private bool _disposed = false;

        //Finalize method for the object, will call Dispose for us
        //to clean up the resources if the user has not called it

        ~TenantRepository()
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