using NUnit.Framework;
using hms.entappsettings.repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hms.entappsettings.context;

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
        }

        // Use to run code afer all tests have run 
        //[OneTimeTearDown]
        //public void TestFixtureTearDown() { }

        #endregion


        // Add or remove [TestCase] attributes for multiple cases
        [TestCase(0, 1)]
        [TestCase(1, 2)]
        [TestCase(6, 3)]
        [TestCase(7, 0)]
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