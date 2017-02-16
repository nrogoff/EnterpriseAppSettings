using NUnit.Framework;
using hms.entappsettings.repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using hms.entappsettings.context;
using hms.entappsettings.model;

namespace hms.entappsettings.repository.Repositories.Tests
{
    [TestFixture]
    [Category("IntegrationTest")]
    public class AppSettingGroupRepositoryTests
    {
        private static AppSettingGroupRepository _appSettingGroupRepository;
        private static EntAppSettingsDbContext _dbContext;

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
            _appSettingGroupRepository = new AppSettingGroupRepository(_dbContext);
           
        }

        // Use to run code after each test has run
        [TearDown]
        public void TearDown()
        {
            _appSettingGroupRepository.Dispose();
            _appSettingGroupRepository = null;
            _dbContext.Dispose();
            _dbContext = null;
        }

        // Use to run code afer all tests have run 
        //[OneTimeTearDown]
        //public void TestFixtureTearDown() { }

        #endregion


        // Add or remove [TestCase] attributes for multiple cases
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(7, 3)]
        [TestCase(8, 0)]
        //[TestCaseSource(typeof(GroupsParentsCaseSource))]
        [Test]
        public void GetAppSettingParents_Exists_Success(int groupId, int expectedCount)
        {
            // ==== Arrange ====

            
            // ==== Act ====
            
            var actual = _appSettingGroupRepository.GetAppSettingGroupParents(groupId).ToList();

            // ==== Assert ====

            Assert.AreEqual(expectedCount, actual.Count);
        }

        [Test]
        public void AddAppSettingGroup_Add_Success()
        {
            // ==== Arrange ====
            var appSettingGroup = new AppSettingGroup
            {
                Group = "TestGroup1",
                Description = "This is part of integration testing",
                ParentGroupId = 1,
                ModifiedBy = "Integartion Test",
                ModifiedDate = DateTime.UtcNow
            };

            // ==== Act ====

            _appSettingGroupRepository.Add(appSettingGroup);
            _appSettingGroupRepository.SaveChanges();

            var actual = _appSettingGroupRepository.GetAll().FirstOrDefault(a => a.Group == appSettingGroup.Group);         

            // ==== Assert ====

            Assert.IsNotNull(actual);
            actual.Group.Should().Be("TestGroup1");
        }

        [Test]
        public void AddAppSettingGroup_AddParentNotExist_Exception()
        {
            // ==== Arrange ====
            var appSettingGroup = new AppSettingGroup
            {
                Group = "TestGroup99",
                Description = "This is part of integration testing",
                ParentGroupId = 99,
                ModifiedBy = "Integartion Test",
                ModifiedDate = DateTime.UtcNow
            };

            // ==== Act ====         
            Action action = () => _appSettingGroupRepository.Add(appSettingGroup);

            // ==== Assert ====
            action.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("Invalid Parent Group Id"));
        }

        [Test]
        public void AddAppSettingGroup_AddParentNull_Exception()
        {
            // ==== Arrange ====
            var appSettingGroup = new AppSettingGroup
            {
                Group = "TestGroup99",
                Description = "This is part of integration testing",
                ParentGroupId = null,
                ModifiedBy = "Integartion Test",
                ModifiedDate = DateTime.UtcNow
            };

            // ==== Act ====         
            Action action = () => _appSettingGroupRepository.Add(appSettingGroup);

            // ==== Assert ====
            action.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("Parent Group Id can't be null"));
        }


        [Test]
        public void UpdateAppSettingGroup_Update_Success()
        {
            // ==== Arrange ====
            var expected = new AppSettingGroup
            {
                AppSettingGroupId = 7,
                ParentGroupId = 6,
                Group = "TestLevel3-4",
                Description = "Updated",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====
            _appSettingGroupRepository.Update(expected);
            _appSettingGroupRepository.SaveChanges();

            var actual = _appSettingGroupRepository.GetById(expected.AppSettingGroupId);

            // ==== Assert ====
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void UpdateAppSettingGroup_NotExists_Exception()
        {
            // ==== Arrange ====
            var expected = new AppSettingGroup
            {
                AppSettingGroupId = 99,
                ParentGroupId = 6,
                Group = "TestLevel3-4",
                Description = "Updated",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====
            _appSettingGroupRepository.Update(expected);
            Action action = () => _appSettingGroupRepository.SaveChanges();

            // ==== Assert ====
            action.ShouldThrowExactly<DbUpdateConcurrencyException>();
        }

        [Test]
        public void DeleteAppSettingGroup_Exists_Success()
        {
            // ==== Arrange ====
            var tobedeleted = _appSettingGroupRepository.GetAll().FirstOrDefault(a => a.Group == "TestGroup1");

            // ==== Act ====
            _appSettingGroupRepository.Delete(tobedeleted);
            _appSettingGroupRepository.SaveChanges();

            var expected = _appSettingGroupRepository.GetAll().FirstOrDefault(a => a.Group == "TestGroup1");

            // ==== Assert ====
            expected.Should().BeNull();
        }

        [Test]
        public void DeleteAppSettingGroup_NotExists_Exeption()
        {
            // ==== Arrange ====
            var expected = new AppSettingGroup
            {
                AppSettingGroupId = 99,
                ParentGroupId = 6,
                Group = "TestLevel3-4",
                Description = "Updated",
                ModifiedDate = DateTime.UtcNow,
                ModifiedBy = "Integration Test"
            };

            // ==== Act ====
            Action action = () => _appSettingGroupRepository.Delete(expected);
            //Action action = () => _appSettingGroupRepository.SaveChanges();
            
            // ==== Assert ====
            action.ShouldThrowExactly<InvalidOperationException>().Where(e => e.Message.StartsWith("The object cannot be deleted"));
        }

    }

    //public class GroupsParentsCaseSource : IEnumerable
    //{
    //    public IEnumerator GetEnumerator()
    //    {
    //        yield return new[] {0, 1};
    //        yield return new[] {1, 2};
    //        yield return new[] {6, 3};
    //        yield return new[] {7, 0};

    //    }
    //}

}