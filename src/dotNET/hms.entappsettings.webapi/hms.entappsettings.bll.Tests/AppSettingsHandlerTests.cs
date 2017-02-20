using NUnit.Framework;
using hms.entappsettings.bll;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using hms.entappsettings.context;
using hms.entappsettings.contracts.Automapper;
using hms.entappsettings.model;
using hms.entappsettings.model.Enums;
using hms.entappsettings.repository.Repositories;

namespace hms.entappsettings.bll.Tests
{
    [TestFixture()]
    public class AppSettingsHandlerTests
    {
        private EntAppSettingsDbContext _dbContext;
        private AppSettingGroupRepository _appSettingGroupRepository;
        private TenantRepository _tenantRepository;
        private AppSettingRepository _appSettingRepository;

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
            Mapper.Initialize(cfg => cfg.AddProfile<EntAppContractsAutoMapperProfile>());
        }

        // Use to run code after each test has run
        [TearDown]
        public void TearDown() { }

        // Use to run code afer all tests have run 
        // [OneTimeTearDown]
        // public void TestFixtureTearDown() { }

        #endregion

        [TestCase(1, 1, false, 3, 0, 0)]
        [TestCase(1, 1, true, 3, 0, 0)]
        [TestCase(2, 3, false, 8, 3, 0)]
        [TestCase(2, 3, true, 9, 3, 1)]
        [TestCase(2, 6, false, 5, 1, 0)]
        [TestCase(2, 6, true, 6, 1, 1)]
        [TestCase(3, 7, false, 7, 1, 0)]
        [TestCase(3, 7, true, 8, 1, 1)]
        [TestCase(3, 5, false, 6, 1, 0)]
        [TestCase(3, 5, true, 7, 1, 1)]
        [Test]
        public void GetWithOverride_Valid_Success(int tenantId, int appSettingGroupId, bool includeInternal, 
            int expectedTotalCount, int expectedOverrideCount, int expectedInternalCount)
        {
            // ==== Arrange ====

            var appSettingsHandler = new AppSettingsHandler(_appSettingRepository, _tenantRepository,_appSettingGroupRepository,Mapper.Instance);

            // ==== Act ====

            var actual = appSettingsHandler.GetAppSettingWithOverrideDtos(tenantId,appSettingGroupId,includeInternal).ToList();

            // ==== Assert ====

            Assert.Multiple(() =>
            {
                actual.Should().HaveCount(expectedTotalCount, "Total count check");
                actual.Where(s => s.Overridden).Should().HaveCount(expectedOverrideCount, "Total overriden count check");
                actual.Where(s => s.IsInternalOnly).Should().HaveCount(expectedInternalCount, "Total internal count check");              
            });

        }

    }
}