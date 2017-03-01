using System;
using System.Collections.Generic;
using MyCouch.Requests;

namespace DatabaseConnectionTester
{
    class Program
    {

        static void Main(string[] args)
        {
            
            //setup our database connection
            using (var db = new MyCouch.MyCouchClient("http://localhost:5984", "test"))
            {


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


                #region Get
                //retrieving database 5
                var responseGet1 = db.Documents.GetAsync("5");
                Console.WriteLine(responseGet1.Result.ToStringDebugVersion() + "\n");
                Console.WriteLine(responseGet1.Result.Content + "\n");

                //retrieving database 1
                var responseGet2 = db.Documents.GetAsync("1");
                Console.WriteLine(responseGet2.Result.ToStringDebugVersion() + "\n");
                Console.WriteLine(responseGet2.Result.Content + "\n");
                #endregion Get


                #region Get View Query

                //Create query to run, the view is already created in couchDB
                var newquery = new QueryViewRequest("artists_albums", "album_by_artists");
                //Configure the query to run with our key
                newquery.Configure(cfg => cfg.Key("Super medium artist"));

                

                //Capture our response
                var responseGetView1 = db.Views.QueryAsync<Album[]>(newquery);

                //print out
                Console.Write(responseGetView1.Result.ToStringDebugVersion() + "\n");

                //Create a temp list to grab some values
                List<string> newList = new List<string>();

                //cycle through each of the results rows
                foreach (var rowResult in responseGetView1.Result.Rows)
                {

                    //asign a temp value to rowresult value
                    var tempRow = rowResult.Value;

                    //cycle throw each item in the temp row
                    foreach (var item in tempRow)
                    {

                        //add the item to the list
                        newList.Add(item.Name);
                    }

                    
                    
                }

                //foreach item in the list print it out
                foreach (var item in newList)
                {
                    Console.WriteLine(item + "\n");
                }




                #endregion Get View Query



                //terminate upon user input
                Console.ReadLine();




            }
            


        }

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

