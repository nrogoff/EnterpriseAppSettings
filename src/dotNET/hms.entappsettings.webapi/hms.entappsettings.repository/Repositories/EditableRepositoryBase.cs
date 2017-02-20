// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.repository
// Filename: EditableRepositoryBase.cs
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using hms.entappsettings.context;
using hms.entappsettings.context.Extensions;

namespace hms.entappsettings.repository.Repositories
{
    /// <summary>
    /// Ediatble Base Repository
    /// </summary>
    /// <typeparam name="T">The Entity that this repository manages</typeparam>
    public abstract class EditableRepositoryBase<T> : ReadOnlyRepositoryBase<T>, IEditableRepository<T> where T : class
    {
        public EditableRepositoryBase(IEntAppSettingsDbContext dbContext): base(dbContext)
        {      
        }

        /// <summary>
        /// Delete a single entity
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Adds an entity with no error handling or model checking. 
        /// Use Add from the repository if available for more defensive
        /// inserts to the data layer.
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity)
        {
            _dbContext.Set<T>().Add(entity);
        }

        /// <summary>
        /// Only really useful for entities fetched from the database
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbContext.Set<T>().Attach(entity);
                entry = _dbContext.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            _dbContext.SaveChangesAsync();
        }
    }
}