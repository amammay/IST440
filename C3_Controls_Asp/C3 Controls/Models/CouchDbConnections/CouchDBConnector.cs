using System;
using System.Collections.Generic;
using System.Web.Configuration;
using MyCouch;
using MyCouch.Requests;
using Newtonsoft.Json;

namespace C3_Controls.Models
{
    public class CouchDbConnector
    {
        #region Public Methods

        /// <summary>
        ///     Start point for connecting to the database
        /// </summary>
        public CouchDbConnector()
        {
            //Initilize 
            WtlMap = new Dictionary<string, WTLItem[]>();
            PttMap = new Dictionary<string, PTTItem[]>();

            //New instance of a mycouchclient
            var myClient = new MyCouchClient(DatabaseAddress, DatabaseName);
            DbConnection(myClient);
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// </summary>
        /// <param name="couchConnection"></param>
        private void DbConnection(MyCouchClient couchConnection)
        {
            using (var db = couchConnection)
            {
                var wtlQuery = new QueryViewRequest("database_query", "wtl_all");
                var pttQuery = new QueryViewRequest("database_query", "ptt_all");

                //Capture our response from the query of our view
                var wtlQueryResponse = db.Views.QueryAsync(wtlQuery);
                var pttQueryResponse = db.Views.QueryAsync(pttQuery);

                Console.WriteLine(wtlQueryResponse.Result.ToStringDebugVersion());
                Console.WriteLine(pttQueryResponse.Result.ToStringDebugVersion());

                //So just to catch up a Dictionary consists of Key, Value pairs
                //Create a new dictionary with our key being a string IE. Contact_Block_Configuration or Operator_Type and etc....
                //Our value being a array of PTTItems, this allows multiple object arays to be brought in from JSON
                var dictionaryValuesWtl = new Dictionary<string, WTLItem[]>();
                var dictionaryValuesPtt = new Dictionary<string, PTTItem[]>();

                //Cycle through our response that we got, specifically the Rows
                foreach (var row in wtlQueryResponse.Result.Rows)
                {
                    //assign a temp itemObjcct to our rows value
                    var itemObject = row.Value;

                    //set our dictionary to the Deserialized object Dictionary consisting of strings and pttitems
                    dictionaryValuesWtl = JsonConvert.DeserializeObject<Dictionary<string, WTLItem[]>>(itemObject);
                }

                //Cycle through our response that we got, specifically the Rows
                foreach (var row in pttQueryResponse.Result.Rows)
                {
                    //assign a temp itemObjcct to our rows value
                    var itemObject = row.Value;

                    //set our dictionary to the Deserialized object Dictionary consisting of strings and pttitems
                    dictionaryValuesPtt = JsonConvert.DeserializeObject<Dictionary<string, PTTItem[]>>(itemObject);
                }

                //Assign our IDictionary values to our local dictionarys
                WtlMap = dictionaryValuesWtl;
                PttMap = dictionaryValuesPtt;
            }
        }

        #endregion Private Methods

        #region Public Fields

        public string DatabaseAddress => WebConfigurationManager.AppSettings["DataBaseAddress"];

        public string DatabaseName => WebConfigurationManager.AppSettings["DataBaseName"];

        public string DataBaseWtl => WebConfigurationManager.AppSettings["C3Controls_WTL"];

        public string DataBasePtt => WebConfigurationManager.AppSettings["C3Controls_PTT"];

        public Dictionary<string, WTLItem[]> WtlMap { get; set; }

        public Dictionary<string, PTTItem[]> PttMap { get; set; }

        #endregion Public Fields
    }
}