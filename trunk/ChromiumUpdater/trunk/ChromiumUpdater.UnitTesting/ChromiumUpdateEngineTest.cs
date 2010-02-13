using ChromiumUpdater.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using ChromiumUpdater.Engine.Schemas;

namespace ChromiumUpdater.UnitTesting
{
    
    
    /// <summary>
    ///This is a test class for ChromiumUpdateEngineTest and is intended
    ///to contain all ChromiumUpdateEngineTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChromiumUpdateEngineTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetChromiumLatestVersionString
        ///</summary>
        [TestMethod()]
        public void GetChromiumLatestVersionStringTest()
        {
            ChromiumUpdateEngine target = new ChromiumUpdateEngine(); 
            string actual;
            actual = target.GetChromiumLatestVersionString();
            Assert.AreNotEqual<String>(actual, String.Empty);
        }

        [TestMethod()]
        public void GetChromiumLatestVersionUpdateXml()
        {
            ChromiumUpdateEngine target = new ChromiumUpdateEngine();
            string actual;
            actual = target.GetChromiumLatestVersionString();
            Assert.AreNotEqual<String>(actual, String.Empty);
            using (Stream s = target.GetChromiumVersionChangeLogDataStream(actual))
            {
                using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                {
                    String xmlData = sr.ReadToEnd();
                }
            }
        }

        [TestMethod()]
        public void GetChromiumLatestVersionUpdateLog()
        {
            try
            {
                ChromiumUpdateEngine target = new ChromiumUpdateEngine();
                string actual;
                actual = target.GetChromiumLatestVersionString();
                Assert.AreNotEqual<String>(actual, String.Empty);
                Log log = target.GetChromiumVersionChangeLogData(actual);
                Assert.IsNotNull(log);
            }
            catch
            {
                throw;
            }
        }

        [TestMethod()]
        public void GetChromiumVersions()
        {
            try
            {
                ChromiumUpdateEngine target = new ChromiumUpdateEngine();
               
                target.GetChromiumVersions();
            }
            catch
            {
                throw;
            }
        }
    }
}
