using MovieLibrary.Controller;
using MovieLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApplicationWithDLL.Presentation
{
    public class Menu
    {
        public static MovieManager _movieManager;
        //static Menu()
        //{
        //    _movieManager = new MovieManager();
        //}
        public static void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("=======================================================");
                Console.WriteLine("\n Simple Movies App Menu:");
                Console.WriteLine("1. Add Movie:");
                Console.WriteLine("2. Edit Movie:");
                Console.WriteLine("3. Find Movie By Id:");
                Console.WriteLine("4. Find Movie By Name:");
                Console.WriteLine("5. Display Movies:");
                Console.WriteLine("6. Remove Movie By Id");
                Console.WriteLine("7. Clear All Movies:");
                Console.WriteLine("8. Exit");

                Console.WriteLine("Enter Choice");
                int option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        AddMovie();
                        break;
                    case 2:
                        EditMovie();
                        break;
                    case 3:
                        FindMovieByID();
                        break;
                    case 4:
                        FindMovieByName();
                        break;
                    case 5:
                        DisplayMovies();
                        break;
                    case 6:
                        RemoveMovieById();
                        break;
                    case 7:
                        ClearAllMovies();
                        break;
                    case 8:
                        Exit();
                        return;
                    default:
                        Console.WriteLine("Invalid Option");
                        break;
                }
            }
        }
        private static void AddMovie() //public
        {
            try
            {
                Console.WriteLine("Enter movie details:");

                Console.WriteLine("Movie ID:");
                int id;

                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    return;
                }

                Console.WriteLine("Movie Name:");
                string name = Console.ReadLine();

                Console.WriteLine("Movie Genre:");
                string genre = Console.ReadLine();

                Console.WriteLine("Movie Year:");
                int year = int.Parse(Console.ReadLine());

                _movieManager.AddMovie(id, name, genre, year);
                Console.WriteLine("Movie added successfully");
            }
            catch (MovieListIsFullException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured {ex.Message}");
            }
        }
        private static void EditMovie()
        {
            try
            {
                Console.WriteLine("Enter Movie Id to Edit:");
                int id;
                // try parse = convert a string to integer  | out = store integer 
                if (!int.TryParse(Console.ReadLine(), out id))
                {

                    return;
                }
                var movie = _movieManager.FindMovieByID(id);
                if (movie == null)
                {
                    Console.WriteLine($"Invalid");
                    return;
                }
                Console.WriteLine("Enter New Name:");
                string newName = Console.ReadLine();

                Console.WriteLine("Enter New Genre:");
                string newGenre = Console.ReadLine();

                Console.WriteLine("Enter New Year:");
                int newYear = int.Parse(Console.ReadLine());

                string result = _movieManager.EditMovie(id, newName, newGenre, newYear);
                Console.WriteLine(result);
            }
            catch (InvalidMovieIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured : {ex.Message}");
            }
        }
        private static void FindMovieByID()
        {
            try
            {
                Console.WriteLine("Enter Movie Id to search:");
                int id = int.Parse(Console.ReadLine());
                var movie = _movieManager.FindMovieByID(id);
                if (movie != null)
                    Console.WriteLine($"{movie.MovieId} , {movie.Name} , {movie.Genre} , {movie.Year}");
            }
            catch (InvalidMovieIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void FindMovieByName()
        {
            try
            {
                Console.WriteLine("Enter Movie Name to search:");
                string name = Console.ReadLine();
                var movies = _movieManager.FindMovieByName(name);
                if (movies.Count > 0)
                    foreach (var movie in movies)
                        Console.WriteLine($"{movie.MovieId} , {movie.Name} , {movie.Genre} , {movie.Year}");
            }
            catch (InvalidMovieNameException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException a)
            { 
                Console.WriteLine(a.Message); 
            }
                
        }
        private static void DisplayMovies()  //public
        {
            try
            {
                var movies = _movieManager.GetMovies();
                if (movies.Count == 0)
                {
                    Console.WriteLine("No Movies Available");
                    return;
                }
                Console.WriteLine("Movies");
                for (int i = 0; i < movies.Count; i++)
                {
                    var movie = movies[i];
                    Console.WriteLine($"{i + 1}.{movie.MovieId} , {movie.Name} , {movie.Genre} , {movie.Year}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured {ex.Message}");
            }
        }
        private static void RemoveMovieById()
        {
            try
            {
                Console.WriteLine("Enter Movie Id to Remove:");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Invalid Mobie ID");
                    return;
                }

                string result = _movieManager.RemoveMovieById(id);
                Console.WriteLine(result);
            }
            catch (InvalidMovieIdException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Occured: {ex.Message}");
            }

        }
        private static void ClearAllMovies() //public
        {
            string result = _movieManager.ClearAllMovies();
            Console.WriteLine(result);
        }
        private static void Exit()
        {
            Console.WriteLine("Exiting the application. Goodbye!");
            Environment.Exit(0);
        }
    }
}