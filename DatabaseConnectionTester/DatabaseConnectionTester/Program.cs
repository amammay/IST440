using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using MyCouch;
using MyCouch.Requests;
using Newtonsoft.Json;

namespace DatabaseConnectionTester
{
    public class PTTItem
    {
        public string id { get; set; }
        public string text { get; set; }
        public string sku { get; set; }
        public string progression { get; set; }
        public int[] voltage_ref_ids { get; set; }
    }

    public class WTLItem
    {
        public string text { get; set; }
        public string sku { get; set; }
        public string progression { get; set; }
        public double price { get; set; }
    }


    public class Program
    {
        public static string DatabaseAddress => ConfigurationManager.AppSettings["DataBaseAddress"];

        public static string DatabaseName => ConfigurationManager.AppSettings["DataBaseName"];

        public static string DatabaseWTL => ConfigurationManager.AppSettings["DataBase_WTL"];

        public static string DatabasePTT => ConfigurationManager.AppSettings["DataBase_PTT"];

        public static IDictionary<string, WTLItem[]> WtlIMap;


        static void Main(string[] args)
        {

            //reworked the connection to be async
            DbConnection().Wait();

            //dispose of our connection
            DbConnection().Dispose();


            //print out the text of each item
            foreach (var mapItem in WtlIMap)
            {

                foreach (var item in mapItem.Value)
                {
                    Console.WriteLine(item.text);
                }

            }

            Console.ReadLine();

        }

        private static async Task DbConnection()
        {
            using (var db = new MyCouchClient(DatabaseAddress, DatabaseName))
            {

                var wtlQuery = new QueryViewRequest("database_query", "wtl_all");

                //Custom view that i have wrote in couchdb
                /*
                 * 
                 * function(doc) {
                 *    if (doc.$doctype !== 'Push To Test')
                 *     return;
                 *    emit(doc.Database.Items, doc.Database.Items.Push_To_Test);
                 *  }
                 */

                //Capture our response from the query of our view
                var wtlQueryResponse = db.Views.QueryAsync(wtlQuery);


                //Print out debug information
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

                WtlIMap = dictionaryValues;

            }

            

        }
    }
}