// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 17
// 
// Project: hms.entappsettings.repository
// Filename: SectionRepository.cs

using System;
using System.Data.Entity;
using System.Linq;
using hms.entappsettings.context;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories
{
    public class AppSettingSectionRepository : EditableRepositoryBase<AppSettingSection> , IAppSettingSectionRepository
    {

        public AppSettingSectionRepository(IEntAppSettingsDbContext dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// Get the App Setting Sections Parents
        /// </summary>
        /// <param name="appSettingSectionId"></param>
        /// <returns></returns>
        public IQueryable<GetParentAppSettingSectionsReturnModel> GetAppSettingSectionParents(int appSettingSectionId)
        {
            return _dbContext.GetParentAppSettingSections(appSettingSectionId);
        }

        /// <summary>
        /// Add an AppSettingSection
        /// </summary>
        /// <param name="appSettingSection"></param>
        public void Add(AppSettingSection appSettingSection)
        {
            if (appSettingSection.ParentSectionId == null)
            {
                throw new ArgumentOutOfRangeException(nameof(appSettingSection), "Parent Section Id can't be null");
            }
            if (!Exists(appSettingSection.ParentSectionId.Value))
            {
                throw new ArgumentOutOfRangeException(nameof(appSettingSection), "Invalid Parent Section Id");
            }

            _dbContext.AppSettingSections.Add(appSettingSection);
        }

        public override void Update(AppSettingSection appSettingSection)
        {
            var entry = _dbContext.Entry(appSettingSection);
            if (entry.State == EntityState.Detached)
            {
                //First query the context
                var existingAppSettingSection = _dbContext.AppSettingSections.Find(appSettingSection.AppSettingSectionId);
                if (existingAppSettingSection != null)
                {
                    //Entity already in context
                    var attachedEntry = _dbContext.Entry(existingAppSettingSection);
                    attachedEntry.CurrentValues.SetValues(appSettingSection);
                }
            }
            else
            {
                base.Update(appSettingSection);
            }
        }

        #region IDisposable

        private bool _disposed = false;

        //Finalize method for the object, will call Dispose for us
        //to clean up the resources if the user has not called it

        ~AppSettingSectionRepository()
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