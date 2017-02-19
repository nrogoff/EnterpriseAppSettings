// Copyright (c) 2017 Hard Medium Soft Ltd.  
// All Rights Reserved.
// 
// Author: Nicholas Rogoff
// Created: 2017 - 02 - 17
// 
// Project: hms.entappsettings.repository.Tests
// Filename: AppSettingSectionRepositoryTests.cs

using System;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using FluentAssertions;
using hms.entappsettings.context;
using NUnit.Framework;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories.Tests
{
    [TestFixture]
    [Category("IntegrationTest")]
    public class AppSettingSectionRepositoryTests
    {
        private EntAppSettingsDbContext _dbContext;
        private AppSettingSectionRepository _appSettingSectionsRepository;

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
            _appSettingSectionsRepository = new AppSettingSectionRepository(_dbContext);

        }

        // Use to run code after each test has run
        [TearDown]
        public void TearDown()
        {
            _appSettingSectionsRepository.Dispose();
            _appSettingSectionsRepository = null;
            _dbContext.Dispose();
            _dbContext = null;
        }

        // Use to run code afer all tests have run 
        //[OneTimeTearDown]
        //public void TestFixtureTearDown() { }

        #endregion

        [Test, Order(10)]
        public void GetAppSettingSections_Exists_Success()
        {
            // ==== Arrange ====

            

            // ==== Act ====
            
            var actual = _appSettingSectionsRepository.GetAll();

            // ==== Assert ====

            actual.Should().HaveCount(5);
        }

        [Test, Order(20)]
        public void GetAppSettingSection_byId_Success()
        {
            // ==== Arrange ====

            
            // ==== Act ====
            
            var actual = _appSettingSectionsRepository.GetById(1);

            // ==== Assert ====

            actual.Should().NotBeNull();
        }

        [Test, Order(30)]
        public void AddAppSettingSection_Valid_Success()
        {
            // ==== Arrange ====

            var expected = new AppSettingSection
            {
               Section = "Integration Testing",
               ParentSectionId = 1,
               Description = "This where App Settings created in Integration test will be made.",
               Ordinality = 10,
               ModifiedDate = DateTime.UtcNow,
               ModifiedBy =  "Integration Test"
            };

            // ==== Act ====
            
            _appSettingSectionsRepository.Add(expected);
            _appSettingSectionsRepository.SaveChanges();

            var actual = _appSettingSectionsRepository.GetAll().FirstOrDefault(s => s.Section == "Integration Testing");

            // ==== Assert ====
            Assert.IsNotNull(actual);
            actual.AppSettingSectionId.Should().Be(6);
        }


        [Test, Order(40)]
        public void AddAppSettingSection_AddParentNotExist_Exception()
        {
            // ==== Arrange ====
            var expected = new AppSettingSection
            {
                Section = "Integration Testing 2",
                ParentSectionId =99,
                Description = "This where App Settings created in Integration test will be made.",
                Ordinality = 10,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====         
            Action action = () => _appSettingSectionsRepository.Add(expected);

            // ==== Assert ====
            action.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("Invalid Parent Section Id"));
        }

        [Test, Order(50)]
        public void AddAppSettingSection_AddParentNull_Exception()
        {
            // ==== Arrange ====
            var expected = new AppSettingSection
            {
                Section = "Integration Testing 3",
                ParentSectionId = null,
                Description = "This where App Settings created in Integration test will be made.",
                Ordinality = 10,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====         
            Action action = () => _appSettingSectionsRepository.Add(expected);

            // ==== Assert ====
            action.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("Parent Section Id can't be null"));
        }

        [Test, Order(60)]
        public void UpdateAppSettingSection_Update_Success()
        {
            // ==== Arrange ====
            var expected = new AppSettingSection
            {
                AppSettingSectionId = 6,
                Section = "Integration Testing",
                ParentSectionId = 1,
                Description = "This where App Settings created in Integration test will be made. Updated",
                Ordinality = 10,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====
            _appSettingSectionsRepository.Update(expected);
            _appSettingSectionsRepository.SaveChanges();

            var actual = _appSettingSectionsRepository.GetById(expected.AppSettingSectionId);

            // ==== Assert ====
            Assert.AreEqual(expected, actual);
        }

        [Test, Order(70)]
        public void UpdateAppSettingSection_NotExists_Exception()
        {
            // ==== Arrange ====
            var expected = new AppSettingSection
            {
                AppSettingSectionId = 99,
                Section = "Integration Testing",
                ParentSectionId = 1,
                Description = "This where App Settings created in Integration test will be made.",
                Ordinality = 90,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====
            _appSettingSectionsRepository.Update(expected);
            Action action = () => _appSettingSectionsRepository.SaveChanges();

            // ==== Assert ====
            action.ShouldThrowExactly<DbUpdateConcurrencyException>();
        }

        [Test, Order(80)]
        public void DeleteAppSettingSection_Exists_Success()
        {
            // ==== Arrange ====
            var tobedeleted = _appSettingSectionsRepository.GetAll().FirstOrDefault(a => a.Section == "Integration Testing");

            // ==== Act ====
            _appSettingSectionsRepository.Delete(tobedeleted);
            _appSettingSectionsRepository.SaveChanges();

            var expected = _appSettingSectionsRepository.GetAll().FirstOrDefault(a => a.Section == "Integration Testing");

            // ==== Assert ====
            expected.Should().BeNull();
        }

        [Test]
        public void DeleteAppSettingSection_NotExists_Exeption()
        {
            // ==== Arrange ====
            var expected = new AppSettingSection
            {
                AppSettingSectionId = 99,
                Section = "Integration Testing",
                ParentSectionId = 1,
                Description = "This where App Settings created in Integration test will be made.",
                Ordinality = 90,
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====
            Action action = () => _appSettingSectionsRepository.Delete(expected);
            //Action action = () => _appSettingGroupRepository.SaveChanges();

            // ==== Assert ====
            action.ShouldThrowExactly<InvalidOperationException>().Where(e => e.Message.StartsWith("The object cannot be deleted"));
        }

    }
}