// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 01 - 28
// 
// Project: hms.entappsettings.context
// Filename: EntAppSettingsDbContext.cs
// ReSharper disable once CheckNamespace

using System.Data.Entity;

namespace hms.entappsettings.context
{
     public partial class EntAppSettingsDbContext
    {
        private static void EntAppSettingsDbContextStaticPartial()
        {
            //System.Data.Entity.Database.SetInitializer<EntAppSettingsDbContext>(new CreateDatabaseIfNotExists<EntAppSettingsDbContext>());

        }
    }
}