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
                var queryPtt = new QueryViewRequest("operator_type", "ptt_all");

                var queryPttResponse = db.Views.QueryAsync(queryPtt);


                Console.WriteLine(queryPttResponse.Result.ToStringDebugVersion());


                Dictionary<string, PTTItem[]> tempValues = new Dictionary<string, PTTItem[]>();


                foreach (var item in queryPttResponse.Result.Rows)
                {
                    var itemObject = item.Value;

                    tempValues=  JsonConvert.DeserializeObject<Dictionary<string, PTTItem[]>>(itemObject);

                    


                }



                








            }
            #endregion DataBase access  


        }

    }


}

