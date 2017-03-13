using System;
using System.Collections;
using System.Collections.Generic;
using MyCouch.Requests;
using System.Configuration;
using Newtonsoft.Json;

namespace DatabaseConnectionTester
{
    public class Program
    {
        public static string DatabaseAddress => ConfigurationManager.AppSettings["DataBaseAddress"];

        public static string DatabaseName => ConfigurationManager.AppSettings["DataBaseName"];

        public static string Database => ConfigurationManager.AppSettings["DataBase"];


        static void Main(string[] args)
        {


            
            //setup our database connection
            using (var db = new MyCouch.MyCouchClient(DatabaseAddress, DatabaseName))
            {

                

                var queryModuleDiameter = new QueryViewRequest("operator_type", "Module_Diameter");
                var queryVoltage = new QueryViewRequest("operator_type", "Voltage");
                var queryPtt = new QueryViewRequest("operator_type", "ptt_all");
               // var queryAll = new QueryViewRequest("operator_type", "Query_All");


                var responseGetView1 = db.Views.QueryAsync<Item>(queryModuleDiameter);
                var responseGetView2 = db.Views.QueryAsync<Item[]>(queryVoltage);
               // var responseGetView3 = db.Views.QueryAsync<Contact_Block_Configuration[]>(queryPtt);

                var testing = db.Views.QueryRawAsync(queryPtt);

                var responseTest = db.Documents.GetAsync("PTT");
                //var responseGetView4 = db.Views.QueryAsync(queryAll);

                var responseJson = responseTest.Result.Content;
                //var responeTesting = testing.Result;

                List<string> results = new List<string>();

                var tempDeser = JsonConvert.DeserializeObject<object>(testing.Result.Content.ToString());


                

                IList<Contact_Block_Configuration> searchResults = new List<Contact_Block_Configuration>();

                foreach (var item in responseJson)
                {
                    Contact_Block_Configuration searchResult =
                        JsonConvert.DeserializeObject<Contact_Block_Configuration>(item.ToString());
                    searchResults.Add(searchResult);

                }





                Console.Write(responseGetView1.Result.ToStringDebugVersion());
                Console.Write(responseGetView2.Result.ToStringDebugVersion());
                //Console.Write(responseGetView3.Result.ToStringDebugVersion());
                Console.Write(responseTest.Result.Content);
               // Console.Write(responseGetView4.Result.ToStringDebugVersion());


                List<Item> itemListdb1 = new List<Item>();
                List<Item> itemListdb2 = new List<Item>();



                foreach (var resultRow in responseGetView1.Result.Rows)
                {

                    var tempRow = resultRow.Value;

                    itemListdb1.Add(tempRow);


                }

                foreach (var resultRow in responseGetView2.Result.Rows)
                {

                    var tempRow2 = resultRow.Value;

                    foreach (var item in tempRow2)
                    {

                        itemListdb1.Add(item);

                    }


                }

                //foreach (var resultRow in responseGetView3.Result.Rows)
                //{

                //    var tempRow3 = resultRow.Value;
                //    foreach (var item3 in tempRow3)
                //    {

                //        itemListdb2.Add(item3);
                //    }

                //}

                //List<TypesTest> playing = new List<TypesTest>();

                //List<Item> newjohn = new List<Item>();


                //foreach (var tempItem in itemListdb1)
                //{


                //    Item Temper = new Item
                //    {
                //        Desc = tempItem.Desc,
                //        Name = tempItem.Name,
                //        Price = tempItem.Price,
                //        SKU = tempItem.SKU

                //    };

                //    newjohn.Add(Temper);

                //    TypesTest newTest = new TypesTest
                //    {
                //        dido = "hello",
                //        Itemsa = newjohn
                //    };

                //    playing.Add(newTest);


                //}









                #region Post

                var artist = new Artist
                {
                    ArtistId = "5",
                    Name = "Foo bar",
                    Albums = new []
                    {
                        new Album{Name = "hello", Tracks = 9}
                    }
                };

                //posting to the database
                //var responsePost = db.Entities.PostAsync(artist);
                //Console.Write(responsePost.Result.ToStringDebugVersion());

                #endregion Post

                

                //#region Get
                ////retrieving database 5
                //var responseGet1 = db.Documents.GetAsync("5");
                //Console.WriteLine(responseGet1.Result.ToStringDebugVersion() + "\n");
                //Console.WriteLine(responseGet1.Result.Content + "\n");

                ////retrieving database 1
                //var responseGet2 = db.Documents.GetAsync("1");
                //Console.WriteLine(responseGet2.Result.ToStringDebugVersion() + "\n");
                //Console.WriteLine(responseGet2.Result.Content + "\n");
                //#endregion Get


                //#region Get View Query

                ////Create query to run, the view is already created in couchDB
                //var newquery = new QueryViewRequest("artists_albums", "album_by_artists");
                ////Configure the query to run with our key
                //newquery.Configure(cfg => cfg.Key("Super medium artist"));

                

                ////Capture our response
                //var responseGetView1 = db.Views.QueryAsync<Album[]>(newquery);

                ////print out
                //Console.Write(responseGetView1.Result.ToStringDebugVersion() + "\n");

                ////Create a temp list to grab some values
                //List<Album> newAlbums = new List<Album>();

                ////cycle through each of the results rows
                //foreach (var rowResult in responseGetView1.Result.Rows)
                //{

                //    //asign a temp value to rowresult value
                //    var tempRow = rowResult.Value;

                //    //cycle throw each item in the temp row
                //    foreach (var item in tempRow)
                //    {

                //        //add the item to the list

                //        Album newAlbum = new Album
                //        {
                //            Name = item.Name,
                //            Tracks = item.Tracks


                //        };

                //        newAlbums.Add(newAlbum);


                //    }

                    
                    
                //}


                //foreach (var album in newAlbums)
                //{
                //    Console.WriteLine(album.Name + "\n" + "Tracks:" + album.Tracks + "\n");
                //}




               // #endregion Get View Query



                //terminate upon user input
                Console.ReadLine();




            }
            


        }

    }



    public class TypesTest
    {
        public string[] Module_Diameter { get; set; }

        public List<Item> Items { get; set; }

    }

    public class Contact_Block_Configuration
    {
        public PttItem newPttitem { get; set; }

    }

    public class PttItem
    {
        public string id { get; set; }
        public string text { get; set; }
        public string sku { get; set; }
        public string progression { get; set; }
        public string[] voltage_ref_ids { get; set; }
    }

    public class Item
    {
        
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Price { get; set; }
        public string Desc { get; set; }

    }


    public class Artist
    {
        public string ArtistId { get; set; }
        public string ArtistRev { get; set; }

        public string Name { get; set; }
        public Album[] Albums { get; set; }
    }

    public class Album
    {
        public string Name { get; set; }
        public int Tracks { get; set; }
    }




}

