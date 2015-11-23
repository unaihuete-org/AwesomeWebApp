using AwesomeWebApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;


namespace AwesomeWebApp.Tests
{
    [TestClass]
    public class AwesomeWebAppTests
    {
        [Ignore]
        [TestMethod]
        public void FailingTest()
        {
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void PassingTest()
        {
            Assert.IsTrue(true);
        }
    }
}
