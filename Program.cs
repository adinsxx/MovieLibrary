using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            //Logging
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            Console.WriteLine("Enter 1 to add a movie.");
            Console.WriteLine("Enter 2 to show the full list.");
            Console.WriteLine("Type anything else to quit.");

            string answer = Console.ReadLine();
            string movieList = "movies.csv"; 

            List<int> MovieID = new List<int>();
            List<String> MovieTitle = new List<string>();
            List<String> MovieGenre = new List<string>();
            StreamReader stream = new StreamReader(movieList); 
            stream.ReadLine();

                //Organizes movie data
                while(!stream.EndOfStream){
                    string line = stream.ReadLine();
                    int position = line.IndexOf('"');
                    string[] movieFull = line.Split(',');
                    MovieID.Add(int.Parse(movieFull[0]));
                    MovieTitle.Add(movieFull[1].Replace("\"", ""));
                    MovieGenre.Add(movieFull[2].Replace("|", ", "));
                    
                }
            //add new movie, gives ID/you enter title and genre
            if (answer == "1")
            {
                //assigns ID
                int movieID = MovieID.Max() + 1; 
                MovieID.Add(movieID);
                //Prompt user for movie title
                Console.WriteLine("What it the name of the movie?");
                string movieTitle = Console.ReadLine();
                HashSet<String> hashet = new HashSet<string>();
                List<string> DuplicateMovies = (List<string>)MovieTitle.Where(e => !hashet.Add(e));
                if (DuplicateMovies.Contains(movieTitle)){
                    logger.Info("This title is a duplicate: {Title}", movieTitle);
                }
                else {
                    movieID = MovieID.Max() + 1;
                    MovieTitle.Add(movieTitle);
                    //Prompt user for genre(s)
                    Console.WriteLine("What genre is the movie? (List all that apply)\n Enter done to exit");
                    string movieGenre = Console.ReadLine();
                    if (movieGenre != "done" && movieGenre.Length > 0){
                        MovieGenre.Add(movieGenre); 
                    }
                }


                stream.Close();
            }
            //Displays all movies 
            else if (answer == "2"){
                for (int i = 0; i < MovieID.Count; i++){
                    Console.WriteLine($"ID: {MovieID[i]}");
                    Console.WriteLine($"Title: {MovieTitle[i]}");
                    Console.WriteLine($"Genre: {MovieGenre[i]}");
                    Console.WriteLine();
                }
            }
        }
    }
}
