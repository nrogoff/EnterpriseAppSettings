using NUnit.Framework;
using hms.entappsettings.repository.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using hms.entappsettings.context;

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

        //Note: Use 'utestnUnit' snippet to insert methods
        [TestCase(false, 13)]
        [TestCase(true, 15)]
        [Test]
        public void GetAllAppSettings_Return_Success(bool includeInternal, int expectedCount)
        {
            // ==== Arrange ====

            
            // ==== Act ====
            
            var actual = _appSettingRepository.GetAllAppSettings(includeInternal);

            // ==== Assert ====

            Assert.AreEqual(expectedCount, actual.Count());
        }

        [TestCase(0, 0, false, 3, 1, 0)]
        [TestCase(0, 0, true, 3, 1, 0)]
        [TestCase(1, 0, false, 3, 1, 0)]
        [TestCase(1, 0, true, 4, 1, 1)]
        [TestCase(1, 1, false, 4, 1, 0)]
        [TestCase(1, 1, true, 5, 1, 1)]
        [TestCase(1, 2, false, 5, 1, 0)]
        [TestCase(1, 2, true, 6, 1, 1)]
        [TestCase(2, 0, false, 4, 2, 0)]
        [TestCase(2, 0, true, 5, 2, 1)]
        [TestCase(2, 4, false, 5, 2, 0)]
        [TestCase(2, 4, true, 6, 2, 1)]
        [TestCase(2, 6, false, 6, 2, 0)]
        [TestCase(2, 6, true, 7, 2, 1)]
        [Test]
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

        [TestCase(99, 0, false)]
        [TestCase(99, 0, true)]
        [TestCase(99, 99, false)]
        [TestCase(99, 99, true)]
        [Test]
        public void GetResultantAppSettings_TenantNotExist_Null(int tenantId, int appGroupId, bool includeInternal)
        {
            // ==== Arrange ====

            Action act = () => _appSettingRepository.GetResultantAppSettings(tenantId, appGroupId, includeInternal);
            
            // ==== Act ====
            
            

            // ==== Assert ====

            act.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("Tenant"));
        }

        [TestCase(0, 99, false)]
        [TestCase(0, 99, true)]
        [Test]
        public void GetResultantAppSettings_GroupNotExist_Null(int tenantId, int appGroupId, bool includeInternal)
        {
            // ==== Arrange ====

            Action act = () => _appSettingRepository.GetResultantAppSettings(tenantId, appGroupId, includeInternal);

            // ==== Act ====



            // ==== Assert ====

            act.ShouldThrowExactly<ArgumentOutOfRangeException>().Where(e => e.Message.StartsWith("App Setting Group"));
        }

    }
}