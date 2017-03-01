using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseConnectionTester
{
    class Program
    {

        


        static void Main(string[] args)
        {


            using (var db = new MyCouch.MyCouchClient("http://localhost:5984", "test"))
            {

                var artist = new Artist
                {
                    ArtistId = "5",
                    Name = "Foo bar",
                    Albums = new []
                    {
                        new Album{Name = "hello", Tracks = 9}
                    }
                };

                //var response = db.Entities.PostAsync(artist);


                

                var response = db.Documents.GetAsync("5");



                //List<string> temp =  response.Result.Content.ToArray();



                string thestring = response.Result.Content.ToString();


                








                Console.Write(response.Result.ToStringDebugVersion());

                Console.WriteLine(thestring);


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

