using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using MyCouch.Requests;


namespace C3_Controls.Models
{
    public class couchDBConnector
    {

        #region Public Fields

        public string DatabaseAddress => WebConfigurationManager.AppSettings["DataBaseAddress"];

        public string DatabaseName => WebConfigurationManager.AppSettings["DataBaseName"];

        public string Database => WebConfigurationManager.AppSettings["DataBase"];

        public List<PricedItem> ItemList { get; set; }
        

        #endregion Public Fields


        public couchDBConnector()
        {
            ItemList = new List<PricedItem>();
            //establishes our connection to the database
            using (var db = new MyCouch.MyCouchClient(DatabaseAddress, DatabaseName))
            {
                

                var queryVoltage = new QueryViewRequest("operator_type", "Voltage");

                var responseGetView2 = db.Views.QueryAsync<PricedItem[]>(queryVoltage);

                

                foreach (var resultRow in responseGetView2.Result.Rows)
                {

                    var tempRow2 = resultRow.Value;

                    foreach (var item in tempRow2)
                    {

                        ItemList.Add(item);

                    }


                }


            }


           


        }


       



    }
}