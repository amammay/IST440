using System;
using C3_Controls.Models.DataStructuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest.UnitTests
{
    [TestClass]
    public class WtlUnitTest
    {

        [TestMethod]
        public void TestWTl()
        {
            WTL_Data tempWtlData = new WTL_Data();


            var returnMap = tempWtlData.WtlMap;


            Assert.IsNotNull(returnMap);

            Console.WriteLine(returnMap.Keys);


        }
    }
}
