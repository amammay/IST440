using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Configuration;
using MyCouch;
using MyCouch.Requests;
using Newtonsoft.Json;


namespace C3_Controls.Models
{
    public class couchDBConnector
    {

        #region Public Fields

        public string DatabaseAddress => WebConfigurationManager.AppSettings["DataBaseAddress"];

        public string DatabaseName => WebConfigurationManager.AppSettings["DataBaseName"];

        public string DataBaseWTL => WebConfigurationManager.AppSettings["C3Controls_WTL"];

        public string DataBasePTT => WebConfigurationManager.AppSettings["C3Controls_PTT"];

        public Dictionary<string, WTLItem[]> WtlMap { get; set; }





        #endregion Public Fields


        public couchDBConnector()
        {
            WtlMap = new Dictionary<string, WTLItem[]>();

            MyCouchClient myClient = new MyCouchClient(DatabaseAddress, DatabaseName);
            DbConnection(myClient);





        }

        private void DbConnection(MyCouchClient couchConnection)
        {
            

            using (var db = couchConnection)
            {
                var wtlQuery = new QueryViewRequest("database_query", "wtl_all");

                //Capture our response from the query of our view
                var wtlQueryResponse = db.Views.QueryAsync(wtlQuery);

                Console.WriteLine(wtlQueryResponse.Result.ToStringDebugVersion());

                //So just to catch up a Dictionary consists of Key, Value pairs
                //Create a new dictionary with our key being a string IE. Contact_Block_Configuration or Operator_Type and etc....
                //Our value being a array of PTTItems, this allows multiple object arays to be brought in from JSON
                var dictionaryValues = new Dictionary<string, WTLItem[]>();

                //Cycle through our response that we got, specifically the Rows
                foreach (var row in wtlQueryResponse.Result.Rows)
                {
                    //assign a temp itemObjcct to our rows value
                    var itemObject = row.Value;

                    //set our dictionary to the Deserialized object Dictionary consisting of strings and pttitems
                    dictionaryValues = JsonConvert.DeserializeObject<Dictionary<string, WTLItem[]>>(itemObject);
                    
                }
                WtlMap = dictionaryValues;



            }

        }





    }
}