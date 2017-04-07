using System;
using C3_Controls.Models.DataStructuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.UnitTests
{
    [TestClass]
    public class PttUnitTest
    {
        private PTT_Data testdata { get; set; }

        [TestMethod]
        public void TestPttData()
        {
            testdata = new PTT_Data();

            var testPttMap = testdata.PttMap;

            Assert.IsNotNull(testPttMap);

            Console.WriteLine("Staus of PTT Data test = Passed");

        }
    }
}
