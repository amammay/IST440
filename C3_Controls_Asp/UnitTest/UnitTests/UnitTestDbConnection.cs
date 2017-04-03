using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using C3_Controls.Models;
using MyCouch;
using System.Configuration;
using C3_Controls.Models.CouchDbConnections;

namespace UnitTest
{
    [TestClass]
    public class UnitTestDbConnection
    {


        [TestMethod]
        public void TestCouchDbConnector()
        {

            CouchDbConnector testConnector = new CouchDbConnector();


            var tempResponse = testConnector.WtlQueryResponse;

            //If this retruns as null then no db connection is there
            Assert.IsNotNull(testConnector.DeserelizeWtl(tempResponse));


        }
    }
}
