using FeedRead;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FeedReadTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var configuration = Helper.GetApplicationConfiguration();
            Assert.IsTrue(configuration.Col1Width > 0);
        }

    }
} 
