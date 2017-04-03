using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Configuration;
using MyCouch;
using MyCouch.Requests;
using MyCouch.Responses;
using Newtonsoft.Json;

namespace C3_Controls.Models.CouchDbConnections
{
    /// <summary>
    /// @author Alex Mammay
    /// @updated 4/2/2017
    /// @email: amm7100@psu.edu
    /// This class acts a database connection and light data sctucturing
    /// </summary>
    public class CouchDbConnector
    {
        #region Public Fields

        public string DatabaseAddress => WebConfigurationManager.AppSettings["DataBaseAddress"];
        public string DatabaseName => WebConfigurationManager.AppSettings["DataBaseName"];
        public string DataBaseWtl => WebConfigurationManager.AppSettings["C3Controls_WTL"];
        public string DataBasePtt => WebConfigurationManager.AppSettings["C3Controls_PTT"];
        public Dictionary<string, WTLItem[]> WtlMap { get; set; }
        public Dictionary<string, PTTItem[]> PttMap { get; set; }
        public MyCouchClient MyClient { get; set; }

        #endregion Public Fields

        #region Private Fields

        public Task<ViewQueryResponse> WtlQueryResponse { get; set; }
        public Task<ViewQueryResponse> PttQueryResponse { get; set; }

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        /// Establish a connection to the database, retrieve our data and leave.
        /// </summary>
        /// <param name="couchConnection"></param>
        private void DbConnection(MyCouchClient couchConnection)
        {
            //establish connection
            using (var db = couchConnection)
            {
                //Create our querys
                var wtlQuery = new QueryViewRequest("database_query", "wtl_all");
                var pttQuery = new QueryViewRequest("database_query", "ptt_all");

                //Capture our response from the query of our view
                WtlQueryResponse = db.Views.QueryAsync(wtlQuery);
                PttQueryResponse = db.Views.QueryAsync(pttQuery);

                //Debug info
                Console.WriteLine(WtlQueryResponse.Result.ToStringDebugVersion());
                Console.WriteLine(PttQueryResponse.Result.ToStringDebugVersion());

                //disconnect us from the database
                db.Connection.Dispose();
            }
        }

        #endregion Private Methods

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
            MyClient = new MyCouchClient(DatabaseAddress, DatabaseName);

            DbConnection(MyClient);

            //Capture our maps
            WtlMap = DeserelizeWtl(WtlQueryResponse);
            PttMap = DeserelizePtt(PttQueryResponse);
        }

        /// <summary>
        ///     Deserizle our WTL items we got from the database
        /// </summary>
        /// <param name="wtlQueryResponse"></param>
        /// <returns> Returns Dictionary of world tower light items </returns>
        public Dictionary<string, WTLItem[]> DeserelizeWtl(Task<ViewQueryResponse> wtlQueryResponse)
        {
            //Create a local dictionary
            var dictionaryValuesWtl = new Dictionary<string, WTLItem[]>();

            //Cycle through our response that we got, specifically the Rows
            foreach (var row in wtlQueryResponse.Result.Rows)
            {
                //assign a temp itemObjcct to our rows value
                var itemObject = row.Value;

                //set our dictionary to the Deserialized object Dictionary consisting of strings and pttitems
                dictionaryValuesWtl = JsonConvert.DeserializeObject<Dictionary<string, WTLItem[]>>(itemObject);
            }

            return dictionaryValuesWtl;
        }

        /// <summary>
        ///     Deserizle our PTT items we got from the database
        /// </summary>
        /// <param name="pttQueryResponse"></param>
        /// <returns> Returns Dictionary of push to test items </returns>
        public Dictionary<string, PTTItem[]> DeserelizePtt(Task<ViewQueryResponse> pttQueryResponse)
        {
            //Create a local dictionary
            var dictionaryValuesPtt = new Dictionary<string, PTTItem[]>();

            //Cycle through our response that we got, specifically the Rows
            foreach (var row in pttQueryResponse.Result.Rows)
            {
                //assign a temp itemObjcct to our rows value
                var itemObject = row.Value;

                //set our dictionary to the Deserialized object Dictionary consisting of strings and pttitems
                dictionaryValuesPtt = JsonConvert.DeserializeObject<Dictionary<string, PTTItem[]>>(itemObject);
            }

            return dictionaryValuesPtt;
        }

        #endregion Public Methods


    }
}