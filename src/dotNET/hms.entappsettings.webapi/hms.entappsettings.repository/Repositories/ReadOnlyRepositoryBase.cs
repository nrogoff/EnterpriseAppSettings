// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.repository
// Filename: ReadOnlyRepositoryBase.cs

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using hms.entappsettings.context;

namespace hms.entappsettings.repository.Repositories
{
    /// <summary>
    /// Read Only Repository Base
    /// </summary>
    /// <typeparam name="T">The Entity that this repository manages</typeparam>
    public abstract class ReadOnlyRepositoryBase<T> : IRepository<T> where T : class
    {
        protected readonly IEntAppSettingsDbContext _dbContext;

        public ReadOnlyRepositoryBase(IEntAppSettingsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Seach for using a predicate
        /// </summary>
        /// <param name="predicate">The search expression</param>
        /// <returns></returns>
        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        /// <summary>
        /// Get entity by it's Id
        /// </summary>
        /// <param name="id">The entities Id to get</param>
        /// <returns></returns>
        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        /// <summary>
        /// Checks if the enity exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return _dbContext.Set<T>().Find(id) != null;
        }
    }
}