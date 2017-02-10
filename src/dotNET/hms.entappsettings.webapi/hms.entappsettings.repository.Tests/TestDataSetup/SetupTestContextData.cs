// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 05
// 
// Project: hms.entappsettings.repository.Tests
// Filename: SetupTestContextData.cs

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using hms.entappsettings.context;
using hms.entappsettings.model;
using hms.entappsettings.model.Enums;
using NUnit.Framework;

namespace hms.entappsettings.repository.Repositories.Tests
{
    /// <summary>
    /// This class will create ACTUAL DATA in the integration DATABASE!!
    /// </summary>
    [SetUpFixture]
    public class SetupDataForIntegrationTests
    {
        [OneTimeSetUp]
        public void CreateData()
        {
            // Set this to true once to clear the database if it's flagging as populated
            // BE REALLY SURE YOU HAVE THE RIGHT DATABASE CONNECTION STRING!!
            bool ForceInitialClearDownofDb = false;
            if (ForceInitialClearDownofDb)
            {
                Console.WriteLine("#### !! Forced - Clearing database...I hope it's not production !! ");
                TearDownTestData();
            }

            Console.WriteLine("#### Creating test data");

            var connString = ConfigurationManager.ConnectionStrings["TESTEntAppSettingDb"].ConnectionString;
            var intDbContext = new EntAppSettingsDbContext(connString);

            //Check database is a test instance
            //if any groups exist then exit
            if (intDbContext.Tenants.Any() || intDbContext.AppSettingTypes.Any() || intDbContext.AppSettingSections.Any()
                || intDbContext.AppSettingGroups.Any() || intDbContext.AppSettings.Any())
                throw new Exception("Wrong Database?!! Some of the key tables still have records in them. Ensure all non audit tables are empty and identity keys reset.");

            PopulateAppSettingGroups(intDbContext);
            PopulateAppSettingSections(intDbContext);
            PopulateAppSettingTypes(intDbContext);
            PopulateTenants(intDbContext);
            PopulateAppSettings(intDbContext);

            //Clear context object
            intDbContext.Dispose();
            intDbContext = null;
        }


        /// <summary>
        /// Truncates all tables and reset identities to zero
        /// </summary>
        [OneTimeTearDown]
        public void TearDownTestData()
        {
            //return;

            Console.WriteLine("#### Tearing down test data");

            var connString = ConfigurationManager.ConnectionStrings["TESTEntAppSettingDb"].ConnectionString;
            var intDbContext = new EntAppSettingsDbContext(connString);

            var tableList = new List<string>()
            {
                "AppSetting",
                "Tenant",
                "AppSettingType",
                "AppSettingSection",
                "AppSettingGroup"
            };

            foreach (var tablename in tableList)
            {
                intDbContext.Database.ExecuteSqlCommand($"DELETE FROM [dbo].[{tablename}]");
                //reset identity cols
                //var reseedValue = tablename == "AppSettingType" ? 0 : -1;
                var reseedValue = 0;
                intDbContext.Database.ExecuteSqlCommand($"DBCC CHECKIDENT ('{tablename}', RESEED, {reseedValue})");
            }

            //Clear audit tables
            intDbContext.ClearDownAuditTables(0);

            //Clear context object
            intDbContext.Dispose();
            intDbContext = null;
        }

        private void PopulateAppSettings(EntAppSettingsDbContext intDbContext)
        {
            var settings = new List<AppSetting>
            {
                new AppSetting
                {
                    AppSettingId = 1,
                    SettingKey = "TestSetting0",
                    TenantId = 1,
                    SettingGroupId = 1,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 0",
                    IsLocked = true,
                    IsInternalOnly = false,
                    Description = "Setting 1 for all",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 2,
                    SettingKey = "TestSetting1",
                    TenantId = 1,
                    SettingGroupId = 1,
                    SettingSectionId = 2,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 1",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Setting 1 for all",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 3,
                    SettingKey = "TestSetting1",
                    TenantId = 2,
                    SettingGroupId = 1,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 1 for Tenant 1",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 3 for Tenant 1 Internal",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 4,
                    SettingKey = "TestSetting3InternalOnly",
                    TenantId = 2,
                    SettingGroupId = 1,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 3 Internal",
                    IsLocked = false,
                    IsInternalOnly = true,
                    Description = "Test Setting 3 Internal",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 5,
                    SettingKey = "TestSetting1",
                    TenantId = 2,
                    SettingGroupId = 2,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 1 at level 2",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 1 at level 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 6,
                    SettingKey = "TestSetting4",
                    TenantId = 2,
                    SettingGroupId = 2,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 4 at level 2",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 4 at level 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 7,
                    SettingKey = "TestSetting1",
                    TenantId = 2,
                    SettingGroupId = 3,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 1 at level 3",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 1 at level 3",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 8,
                    SettingKey = "TestSetting5",
                    TenantId = 2,
                    SettingGroupId = 3,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 5 at level 3",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 5 at level 3",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 15,
                    SettingKey = "TestSetting5",
                    TenantId = 2,
                    SettingGroupId = 6,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 5 at level 3",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 5 at level 3",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 9,
                    SettingKey = "TestSetting1",
                    TenantId = 3,
                    SettingGroupId = 1,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 1 for Tenant 2",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 1 for Tenant 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 10,
                    SettingKey = "TestSetting6",
                    TenantId = 3,
                    SettingGroupId = 5,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 6 for Tenant 2",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 6 for Tenant 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 11,
                    SettingKey = "TestSetting7",
                    TenantId = 3,
                    SettingGroupId = 7,
                    SettingSectionId = 2,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 7",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Test Setting 7 tenant 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 12,
                    SettingKey = "TestSetting8Internal",
                    TenantId = 3,
                    SettingGroupId = 1,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 8 Internal",
                    IsLocked = false,
                    IsInternalOnly = true,
                    Description = "Test Setting 8 Internal tenant 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 13,
                    SettingKey = "TestSetting12",
                    TenantId = 3,
                    SettingGroupId = 1,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 12",
                    IsLocked = true,
                    IsInternalOnly = false,
                    Description = "Setting 12 for tenant 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSetting
                {
                    AppSettingId = 14,
                    SettingKey = "TestSetting13",
                    TenantId = 1,
                    SettingGroupId = 1,
                    SettingSectionId = 1,
                    TypeId = SettingType.TEXT,
                    SettingValue = "Test Setting 13",
                    IsLocked = false,
                    IsInternalOnly = false,
                    Description = "Setting 13 for all",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                }
            };
            intDbContext.AppSettings.AddRange(settings);
            intDbContext.SaveChanges();
        }

        private void PopulateTenants(EntAppSettingsDbContext intDbContext)
        {
            var tenants = new List<Tenant>
            {
                new Tenant
                {
                    TenantId = 1,
                    TenantCode = "PLATFORM",
                    TenantName = "Platform",
                    TenantDescription = "Applies to all tenants",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new Tenant
                {
                    TenantId = 2,
                    TenantCode = "TESTTENANT1",
                    TenantName = "Test Tenant 1",
                    TenantDescription = "Integration Test Tenant 1",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new Tenant
                {
                    TenantId = 3,
                    TenantCode = "TESTTENANT2",
                    TenantName = "Test Tenant 2",
                    TenantDescription = "Integration Test Tenant 2",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                }
            };

            intDbContext.Tenants.AddRange(tenants);
            intDbContext.SaveChanges();
        }

        private void PopulateAppSettingSections(EntAppSettingsDbContext intDbContext)
        {
            var sections = new List<AppSettingSection>
            {
                new AppSettingSection
                {
                    AppSettingSectionId = 1,
                    ParentSectionId = null,
                    Section = "General",
                    Description = "Platform general settings",
                    Ordinality = 0,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingSection
                {
                    AppSettingSectionId = 2,
                    ParentSectionId = 5,
                    Section = "Versions",
                    Description = "Version Info. Should be under ",
                    Ordinality = 0,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingSection
                {
                    AppSettingSectionId = 3,
                    ParentSectionId = 1,
                    Section = "General-Part-2",
                    Description = "Subsection 2 of general",
                    Ordinality = 100,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingSection
                {
                    AppSettingSectionId = 4,
                    ParentSectionId = 1,
                    Section = "General-Part-1",
                    Description = "Subsection 1 of general",
                    Ordinality = 50,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingSection
                {
                    AppSettingSectionId = 5,
                    ParentSectionId = null,
                    Section = "About",
                    Description = "Subsection 1 of general",
                    Ordinality = 50,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                }
            };
            intDbContext.AppSettingSections.AddRange(sections);
            intDbContext.SaveChanges();
        }

        private void PopulateAppSettingTypes(IEntAppSettingsDbContext intDbContext)
        {
            var appSettingTypes = new List<AppSettingType>
            {
                new AppSettingType{AppSettingTypeId = SettingType.TEXT, AppSettingType_ = SettingType.TEXT.ToString("G"), AppSettingTypeDescription = "Text Content", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" },
                new AppSettingType{AppSettingTypeId = SettingType.INTEGER, AppSettingType_ = SettingType.INTEGER.ToString("G"), AppSettingTypeDescription = "Integer (whole number) only", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" },
                new AppSettingType{AppSettingTypeId = SettingType.DECIMAL, AppSettingType_ = SettingType.DECIMAL.ToString("G"), AppSettingTypeDescription = "Numbers that are not integers", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" },
                new AppSettingType{AppSettingTypeId = SettingType.BOOL, AppSettingType_ = SettingType.BOOL.ToString("G"), AppSettingTypeDescription = "Boolean content (true = 1, false = 0)", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" },
                new AppSettingType{AppSettingTypeId = SettingType.CSV, AppSettingType_ = SettingType.CSV.ToString("G"), AppSettingTypeDescription = "Comma Seperated List. Use double quotes for fields that require it.", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" },
                new AppSettingType{AppSettingTypeId = SettingType.HTML, AppSettingType_ = SettingType.HTML.ToString("G"), AppSettingTypeDescription = "HTML Content", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" },
                new AppSettingType{AppSettingTypeId = SettingType.JSON, AppSettingType_ = SettingType.JSON.ToString("G"), AppSettingTypeDescription = "JSON Content", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" },
                new AppSettingType{AppSettingTypeId = SettingType.XML, AppSettingType_ = SettingType.XML.ToString("G"), AppSettingTypeDescription = "XML Content", ModifiedDate = DateTime.UtcNow, ModifiedBy = "Integration Test" }
            };
            intDbContext.AppSettingTypes.AddRange(appSettingTypes);
            intDbContext.SaveChanges();
        }

        private void PopulateAppSettingGroups(IEntAppSettingsDbContext intDbContext)
        {
            // Structure is
            // 0 = \Core
            // 1 = \Core\TestLevel2-1
            // 2 = \Core\TestLevel2-1\TestLevel3-1
            // 3 = \Core\TestLevel2-1\TestLevel3-2
            // 5 = \Core\TestLevel2-2
            // 4 = \Core\TestLevel2-2\TestLevel3-3
            // 6 = \Core\TestLevel2-2\TestLevel3-4
            //
            //Where TestLevel3-3 has a higher Id than TestLevel2-2

            var appSettingGroups = new List<AppSettingGroup>
            {
                new AppSettingGroup
                {
                    AppSettingGroupId = 1,
                    ParentGroupId = null,
                    Group = "Core",
                    Description = "Settings apply to ALL consumers and is the root of all inherited settings.",
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingGroup
                {
                    AppSettingGroupId = 2,
                    ParentGroupId = 1,
                    Group = "TestLevel2-1",
                    Description = String.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingGroup
                {
                    AppSettingGroupId = 3,
                    ParentGroupId = 2,
                    Group = "TestLevel3-1",
                    Description = String.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingGroup
                {
                    AppSettingGroupId = 4,
                    ParentGroupId = 2,
                    Group = "TestLevel3-2",
                    Description = String.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingGroup
                {
                    AppSettingGroupId = 5,
                    ParentGroupId = 6,
                    Group = "TestLevel3-3",
                    Description = String.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingGroup
                {
                    AppSettingGroupId = 6,
                    ParentGroupId = 1,
                    Group = "TestLevel2-2",
                    Description = String.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                },
                new AppSettingGroup
                {
                    AppSettingGroupId = 7,
                    ParentGroupId = 6,
                    Group = "TestLevel3-4",
                    Description = String.Empty,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedBy = "Integration Test"
                }
            };

            intDbContext.AppSettingGroups.AddRange(appSettingGroups);
            intDbContext.SaveChanges();
        }
    }
}