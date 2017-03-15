using System;
using System.Collections;
using System.Collections.Generic;
using MyCouch.Requests;
using System.Configuration;
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


    public class Program
    {
        public static string DatabaseAddress => ConfigurationManager.AppSettings["DataBaseAddress"];

        public static string DatabaseName => ConfigurationManager.AppSettings["DataBaseName"];

        public static string Database => ConfigurationManager.AppSettings["DataBase"];

        


        static void Main(string[] args)
        {


            #region DataBase access

            //setup our database connection
            using (var db = new MyCouch.MyCouchClient(DatabaseAddress, DatabaseName))
            {

                var queryModuleDiameter = new QueryViewRequest("operator_type", "Module_Diameter");
                var queryVoltage = new QueryViewRequest("operator_type", "Voltage");
                
                
                //Custom view that i have wrote in couchdb
                /*
                 * 
                 * function(doc) {
                 *    if (doc.$doctype !== 'Push To Test')
                 *     return;
                 *    emit(doc.Database.Items, doc.Database.Items.Push_To_Test);
                 *  }
                 */

                var queryPtt = new QueryViewRequest("operator_type", "ptt_all");

                //Capture our response from the query of our view
                var queryPttResponse = db.Views.QueryAsync(queryPtt);
                
                //Print out debug information
                Console.WriteLine(queryPttResponse.Result.ToStringDebugVersion());

                //So just to catch up a Dictionary consists of Key, Value pairs
                //Create a new dictionary with our key being a string IE. Contact_Block_Configuration or Operator_Type and etc....
                //Our value being a array of PTTItems, this allows multiple object arays to be brought in from JSON
                Dictionary<string, PTTItem[]> dictionaryValues = new Dictionary<string, PTTItem[]>();

                //Cycle through our response that we got, specifically the Rows
                foreach (var row in queryPttResponse.Result.Rows)
                {
                    //assign a temp itemObjcct to our rows value
                    var itemObject = row.Value;

                    //set our dictionary to the Deserialized object Dictionary consisting of strings and pttitems
                    dictionaryValues =  JsonConvert.DeserializeObject<Dictionary<string, PTTItem[]>>(itemObject);

                }


                #region Testing ground
                
                List<PTTItem[]> newPttItems = new List<PTTItem[]>();

                foreach (var value in dictionaryValues)
                {

                    var tempValue = value.Value;

                    if (value.Key == "Contact_Block_Configuration")
                    {

                       newPttItems.Add(tempValue);


                    }


                }


                
                foreach (var pttItem in newPttItems)
                {

                    foreach (var thing in pttItem)
                    {
                        Console.WriteLine(thing.progression +"\n" + 
                                            thing.id + "\n" 
                                            + thing.sku + "\n" 
                                            + thing.text + "\n"
                                            + thing.voltage_ref_ids);
                    }


                    

                }
                #endregion Testing Ground

                Console.ReadLine();











            }
            #endregion DataBase access  


        }

    }


}

