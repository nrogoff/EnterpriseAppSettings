// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 17
// 
// Project: hms.entappsettings.repository.Tests
// Filename: TenantRepositoryTests.cs

using System;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using hms.entappsettings.context;
using hms.entappsettings.model;
using NUnit.Framework;

namespace hms.entappsettings.repository.Repositories.Tests
{
    [TestFixture]
    [Category("IntegrationTest")]
    public class TenantRepositoryTests
    {
        private EntAppSettingsDbContext _dbContext;
        private TenantRepository _tenantRepository;

        #region Additional test attributes

        // You can use the following additional attributes as you support your tests:

        // Use to run code Before any tests in a class have run
        //[OneTimeSetUp]
        //public static void TestFixtureSetup()
        //{
        //}

        // Use to run code before each test in the class
        [SetUp]
        public void Setup()
        {
            var connString = ConfigurationManager.ConnectionStrings["TESTEntAppSettingDb"].ConnectionString;
            _dbContext = new EntAppSettingsDbContext(connString);
            _tenantRepository = new TenantRepository(_dbContext);

        }

        // Use to run code after each test has run
        [TearDown]
        public void TearDown()
        {
            _tenantRepository.Dispose();
            _tenantRepository = null;
            _dbContext.Dispose();
            _dbContext = null;
        }

        // Use to run code afer all tests have run 
        //[OneTimeTearDown]
        //public void TestFixtureTearDown() { }

        #endregion

        [Test, Order(10)]
        public void GetAllTenants_Get_Success()
        {
            // ==== Arrange ====


            // ==== Act ====
            
            var actual = _tenantRepository.GetAll();

            // ==== Assert ====

            actual.Should().HaveCount(3);
        }

        [Test, Order(20)]
        public void AddTenant_Add_Success()
        {
            // ==== Arrange ====

            var expected = new Tenant
            {
                TenantName = "TestIntegrationTenant",
                TenantCode = "TESTINT1",
                TenantDescription = "This is a tenant created during integartion tests"
            };

            // ==== Act ====

            _tenantRepository.Add(expected);
            _tenantRepository.SaveChanges();

            var actual = _tenantRepository.GetAll().FirstOrDefault(t => t.TenantCode == expected.TenantCode);
            // ==== Assert ====
            
            Assert.AreEqual(expected, actual);
        }

        [Test, Order(30)]
        public void UpdateTenant_Update_Success()
        {
            // ==== Arrange ====

            var expected = new Tenant
            {
                TenantId = 4,
                TenantName = "TestIntegrationTenant",
                TenantCode = "TESTINT1",
                TenantDescription = "This is a tenant created during integration tests. Updated 1.",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====
            
            _tenantRepository.Update(expected);
            _tenantRepository.SaveChanges();

            var actual = _tenantRepository.GetById(expected.TenantId);

            // ==== Assert ====
            actual.ShouldBeEquivalentTo(expected);
        }

        [Test, Order(31)]
        public void UpdateTenant_UpdateDetached_Success()
        {
            // ==== Arrange ====

            var expected = new Tenant
            {
                TenantId = 4,
                TenantName = "TestIntegrationTenant",
                TenantCode = "TESTINT1",
                TenantDescription = "This is a tenant created during integration tests. Updated 2.",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };
            _tenantRepository.Update(expected);
            _tenantRepository.SaveChanges();

            // ==== Act ====

            //2nd update same context
            var expected2 = new Tenant
            {
                TenantId = 4,
                TenantName = "TestIntegrationTenant",
                TenantCode = "TESTINT1",
                TenantDescription = "This is a tenant created during integration tests. Updated 3.",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };
            _tenantRepository.Update(expected2);
            _tenantRepository.SaveChanges();

            var actual = _tenantRepository.GetById(expected2.TenantId);

            // ==== Assert ====

            actual.ShouldBeEquivalentTo(expected2);
        }

        [Test, Order(40)]
        public void DeleteTenant_Exists_Success()
        {
            // ==== Arrange ====

            var tobedeleted = _tenantRepository.GetAll().FirstOrDefault(t => t.TenantCode == "TESTINT1");

            // ==== Act ====

            _tenantRepository.Delete(tobedeleted);
            _tenantRepository.SaveChanges();

            var actual = _tenantRepository.GetAll().FirstOrDefault(t => t.TenantCode == "TESTINT1");

            // ==== Assert ====

            actual.Should().BeNull();
        }


        [Test, Order(50)]
        public void DeleteTenant_NotExists_Exception()
        {
            // ==== Arrange ====

            var tobedeleted = new Tenant
            {
                TenantId = 4,
                TenantName = "TestIntegrationTenant",
                TenantCode = "TESTINT1",
                TenantDescription = "This is a tenant created during integartion tests. Updated.",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };


            // ==== Act ====

            Action action = () => _tenantRepository.Delete(tobedeleted);

            // ==== Assert ====

            action.ShouldThrowExactly<InvalidOperationException>().Where(e => e.Message.StartsWith("The object cannot be deleted"));
        }


    }
}