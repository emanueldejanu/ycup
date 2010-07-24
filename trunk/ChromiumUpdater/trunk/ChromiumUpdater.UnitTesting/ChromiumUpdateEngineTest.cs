using ChromiumUpdater.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text;
using ChromiumUpdater.Engine.Schemas;
using System.Collections.Generic;
using System.Diagnostics;

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
        public void GetChromiumRegistryInfoTest()
        {
            IChromiumUpdateEngine target = ChromiumUpdateEngineFactory.CreateInstance(); 
            ChromiumRegistryInfo actual;
            actual = target.GetChromiumRegistryInfo();
        }

        /// <summary>
        ///A test for DownloadChromiumTest
        ///</summary>
        [TestMethod()]
        public void DownloadChromiumTest()
        {
            try
            {
                String fileName = Path.Combine(Path.GetTempPath(), "mini_installer.exe");

                IChromiumUpdateEngine target = ChromiumUpdateEngineFactory.CreateInstance();
                string actual;
                actual = target.GetChromiumLatestVersionString();
                target.DownloadChromiumInstaller(Path.GetTempPath(), actual, false, (x) =>
                {
                    Trace.Write(String.Format("", x.ProgressPercentage));
                    Trace.Write(String.Format("", x.TotalBytesToReceive));
                    Trace.Write(String.Format("", x.BytesReceived));
                    return true;
                }
                );
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        ///A test for GetChromiumLatestVersionString
        ///</summary>
        [TestMethod()]
        public void GetChromiumLatestVersionStringTest()
        {
            IChromiumUpdateEngine target = ChromiumUpdateEngineFactory.CreateInstance(); ; 
            string actual;
            actual = target.GetChromiumLatestVersionString();
            Assert.AreNotEqual<String>(actual, String.Empty);
        }

          [TestMethod()]
        public void GetChromiumLatestVersionUpdateLog()
        {
            try
            {
                IChromiumUpdateEngine target = ChromiumUpdateEngineFactory.CreateInstance();
                string actual;
                actual = target.GetChromiumLatestVersionString();
                Assert.AreNotEqual<String>(actual, String.Empty);
                ChangeLog log = target.GetChromiumVersionChangeLogData(actual);
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
                IChromiumUpdateEngine target = ChromiumUpdateEngineFactory.CreateInstance();
               
                IEnumerable<String> results = target.GetChromiumVersions();
                Assert.IsNotNull(results);
                foreach (var version in results)
                {
                    var log = target.GetChromiumVersionChangeLogData(version);
                }
            }
            catch
            {
                throw;
            }
        }

       
    }
}
