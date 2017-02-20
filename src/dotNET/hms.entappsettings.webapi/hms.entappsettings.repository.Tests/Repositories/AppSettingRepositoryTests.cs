using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;
using FluentAssertions;
using hms.entappsettings.context;
using hms.entappsettings.model;
using hms.entappsettings.model.Enums;

namespace hms.entappsettings.repository.Repositories.Tests
{
    [TestFixture]
    [Category("IntegrationTest")]
    public class AppSettingRepositoryTests
    {
        private EntAppSettingsDbContext _dbContext;
        private AppSettingRepository _appSettingRepository;
        private AppSettingGroupRepository _appSettingGroupRepository;
        private TenantRepository _tenantRepository;

        #region Additional test attributes

        // You can use the following additional attributes as you support your tests:

        // Use to run code Before any tests in a class have run
        // [OneTimeSetUp]
        // public static void TestFixtureSetup() { }

        // Use to run code before each test in the class
        [SetUp]
        public void Setup()
        {
            var connString = ConfigurationManager.ConnectionStrings["TESTEntAppSettingDb"].ConnectionString;
            _dbContext = new EntAppSettingsDbContext(connString);
            _appSettingGroupRepository = new AppSettingGroupRepository(_dbContext);
            _tenantRepository = new TenantRepository(_dbContext);
            _appSettingRepository = new AppSettingRepository(_dbContext, _appSettingGroupRepository, _tenantRepository);

        }

        // Use to run code after each test has run
        [TearDown]
        public void TearDown()
        {
            _appSettingRepository.Dispose();
            _appSettingRepository = null;
            _dbContext.Dispose();
            _dbContext = null;

        }

        // Use to run code afer all tests have run 
        // [OneTimeTearDown]
        // public void TestFixtureTearDown() { }

        #endregion

        [TestCase(false, 13)]
        [TestCase(true, 15)]
        [Test, Order(10)]
        public void GetAllAppSettings_Return_Success(bool includeInternal, int expectedCount)
        {
            // ==== Arrange ====

            
            // ==== Act ====
            
            var actual = _appSettingRepository.GetAllAppSettings(includeInternal);

            // ==== Assert ====

            Assert.AreEqual(expectedCount, actual.Count());
        }

        [TestCase(1, 1, false, 3, 1, 0)]
        [TestCase(1, 1, true, 3, 1, 0)]
        [TestCase(2, 1, false, 3, 1, 0)]
        [TestCase(2, 1, true, 4, 1, 1)]
        [TestCase(2, 2, false, 4, 1, 0)]
        [TestCase(2, 2, true, 5, 1, 1)]
        [TestCase(2, 3, false, 5, 1, 0)]
        [TestCase(2, 3, true, 6, 1, 1)]
        [TestCase(3, 1, false, 4, 2, 0)]
        [TestCase(3, 1, true, 5, 2, 1)]
        [TestCase(3, 5, false, 5, 2, 0)]
        [TestCase(3, 5, true, 6, 2, 1)]
        [TestCase(3, 7, false, 6, 2, 0)]
        [TestCase(3, 7, true, 7, 2, 1)]
        [Test, Order(20)]
        public void GetResultantAppSettings_Return_Success(int tenantId, int appGroupId, bool includeInternal, 
            int expectedCount, int expectedLockedCount, int expectedInternalCount)
        {
            // ==== Arrange ====


            // ==== Act ====

            var actual = _appSettingRepository.GetResultantAppSettings(tenantId,appGroupId,includeInternal).ToList();

            // ==== Assert ====
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedCount, actual.Count, "Total count mismatch");
                Assert.AreEqual(expectedLockedCount, actual.Count(s => s.IsLocked), "Locked count mismatch");
                Assert.AreEqual(expectedInternalCount, actual.Count(s => s.IsInternalOnly), "InternalOnly count mismatch");                
            });
        }

        [TestCase(99, 1, false)]
        [TestCase(99, 1, true)]
        [TestCase(99, 99, false)]
        [TestCase(99, 99, true)]
        [Test, Order(30)]
        public void GetResultantAppSettings_TenantNotExist_Null(int tenantId, int appGroupId, bool includeInternal)
        {
            // ==== Arrange ====

            Action act = () => _appSettingRepository.GetResultantAppSettings(tenantId, appGroupId, includeInternal);
            
            // ==== Act ====
            
            

            // ==== Assert ====

            act.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("Tenant"));
        }

        [TestCase(1, 99, false)]
        [TestCase(1, 99, true)]
        [Test, Order(40)]
        public void GetResultantAppSettings_GroupNotExist_Null(int tenantId, int appGroupId, bool includeInternal)
        {
            // ==== Arrange ====

            Action act = () => _appSettingRepository.GetResultantAppSettings(tenantId, appGroupId, includeInternal);

            // ==== Act ====



            // ==== Assert ====

            act.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("App Setting Group"));
        }

        [Test, Order(50)]
        public void AddAppSetting_Valid_Success()
        {
            // ==== Arrange ====

            var expected = new AppSetting
            {
                SettingKey = "INTTEST1",
                Description = "Integration Test App Setting 1",
                SettingGroupId = 5,
                SettingSectionId = 4,
                TenantId = 3,
                TypeId = SettingType.TEXT,
                IsInternalOnly = false,
                IsLocked = false,
                ModifiedBy = "Integration Test",
                ModifiedDate = DateTime.UtcNow
            };

            // ==== Act ====

            _appSettingRepository.Add(expected);
            _appSettingRepository.SaveChanges();

            var actual = _appSettingRepository.GetAll().FirstOrDefault(s => s.SettingKey == "INTTEST1");
            // ==== Assert ====


            Assert.AreEqual(expected, actual);
        }

        [Test, Order(60)]
        public void AddAppSetting_NameWithSpaces_Exception()
        {
            // ==== Arrange ====
            var expected = new AppSetting
            {
                SettingKey = "Name with spaces",
                Description = "Integration Test App Setting 1",
                SettingGroupId = 5,
                SettingSectionId = 4,
                TenantId = 3,
                TypeId = SettingType.TEXT,
                IsInternalOnly = false,
                IsLocked = false,
                ModifiedBy = "Integration Test",
                ModifiedDate = DateTime.UtcNow
            };

            // ==== Act ====
            Action action = () => { _appSettingRepository.Add(expected); };
            

            // ==== Assert ====
            action.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("The App Setting Key cannot contain spaces"));
        }

        [Test, Order(70)]
        public void UpdateAppSetting_Valid_Success()
        {
            // ==== Arrange ====
            var expected = _appSettingRepository.GetById(15);
            expected.Description = "Updated!";

            // ==== Act ====

            _appSettingRepository.Update(expected);
            _appSettingRepository.SaveChanges();

            var actual = _appSettingRepository.GetById(expected.AppSettingId);

            // ==== Assert ====

            Assert.AreEqual(expected, actual);

        }


        [Test, Order(80)]
        public void DeleteAppSetting_Valid_Success()
        {
            // ==== Arrange ====
            var tobedeleted = _appSettingRepository.GetAll().FirstOrDefault(s => s.SettingKey == "INTTEST1");


            // ==== Act ====

            _appSettingRepository.Delete(tobedeleted);
            _appSettingRepository.SaveChanges();

            var actual = _appSettingRepository.GetAll().FirstOrDefault(s => s.SettingKey == "INTTEST1");

            // ==== Assert ====

            actual.Should().BeNull();

        }


        [Test, Order(90)]
        public void DeleteTenant_NotExists_Exception()
        {
            // ==== Arrange ====

            var tobedeleted = new AppSetting
            {
                AppSettingId = 15,
                SettingKey = "INTTEST1",
                Description = "Integration Test App Setting 1 - Updated",
                SettingGroupId = 5,
                SettingSectionId = 4,
                TenantId = 3,
                TypeId = SettingType.TEXT,
                IsInternalOnly = false,
                IsLocked = false,
                ModifiedBy = "Integration Test",
                ModifiedDate = DateTime.UtcNow
            };


            // ==== Act ====

            Action action = () => _appSettingRepository.Delete(tobedeleted);

            // ==== Assert ====

            action.ShouldThrowExactly<InvalidOperationException>().Where(e => e.Message.StartsWith("The object cannot be deleted"));
        }



    }
}