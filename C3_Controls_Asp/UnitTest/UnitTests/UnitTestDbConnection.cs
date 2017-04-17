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
        private CouchDbConnector testConnector { get; set; }

        [TestMethod]
        public void TestCouchDbConnector()
        {

            CouchDbConnector testConnector = new CouchDbConnector();


            var tempWtlResponse = testConnector.WtlQueryResponse;
            var tempPttResponse = testConnector.WtlQueryResponse;

            //If this retruns as null then no db connection is there
            Assert.IsNotNull(testConnector.DeserelizeWtl(tempWtlResponse));

            Console.WriteLine("Status of the World tower light database " + tempWtlResponse.Result.StatusCode);

            Assert.IsNotNull(testConnector.DeserelizePtt(tempPttResponse));

            Console.WriteLine("Status of the Push to Test database " + tempPttResponse.Result.StatusCode);




        }

    

    }
}
