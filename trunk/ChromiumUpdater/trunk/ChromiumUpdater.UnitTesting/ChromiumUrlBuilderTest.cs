using ChromiumUpdater.Engine;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ChromiumUpdater.UnitTesting
{
    
    
    /// <summary>
    ///This is a test class for ChromiumUrlBuilderTest and is intended
    ///to contain all ChromiumUrlBuilderTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ChromiumUrlBuilderTest
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
        ///A test for GetChromeLatestVersionDescription
        ///</summary>
        [TestMethod()]
        public void GetChromeLatestVersionDescriptionTest()
        {
            ChromiumUrlBuilder target = new ChromiumUrlBuilder(); 
            Uri actual;
            Uri expected = new Uri("http://build.chromium.org/buildbot/snapshots/chromium-rel-xp/38312/mini_installer.exe");
            actual = target.GetUrlToVersionMiniInstaller("38312");
            Assert.AreEqual<Uri>(expected, actual);
        }

        /// <summary>
        ///A test for GetUrlToLatestChromiumVersionDescription
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Chromium.Update.Engine.dll")]
        public void GetUrlToLatestChromiumVersionDescriptionTest()
        {
            ChromiumUrlBuilder target = new ChromiumUrlBuilder(); 
            System.Uri expected = new Uri("http://build.chromium.org/buildbot/snapshots/chromium-rel-xp/LATEST"); 
            System.Uri actual;
            actual = target.GetUrlToLatestChromiumVersionDescription();
            Assert.AreEqual(expected, actual);
        }
    }
}
