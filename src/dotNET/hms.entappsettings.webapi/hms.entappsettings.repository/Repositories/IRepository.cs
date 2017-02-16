// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 30
// 
// Project: hms.entappsettings.repository
// Filename: IRepository.cs

using System;
using System.Linq;
using System.Linq.Expressions;

namespace hms.entappsettings.repository.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        T GetById(int id);
        bool Exists(int id);
    }
}