using MovieLibrary.Exceptions;
using MovieLibrary.model;
using MovieLibrary.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Controller
{
    public class MovieManager
    {
        public List<Movie> movies;
        public const int MaxMovies = 5;

        public MovieManager()
        {
            movies = SerializeDeserialize.DeserializationMovies();
            if (movies == null)
            {
                movies = new List<Movie>();
            }
        }
        public List<Movie> GetMovies()
        {
            return movies;
        }
        public void AddMovie(int id, string name, string genre, int year)
        {
            if (IsMovieListFull())
            {
                throw new MovieListIsFullException("Movie list is full");
            }
            Movie movie = new Movie(id, name, genre, year);
            movies.Add(movie);
            SerializeDeserialize.SerializeMovies(movies);
        }
        public bool IsMovieListFull()
        {
            return movies.Count >= MaxMovies;
        }
        public string EditMovie(int id, string newName, string newGenre, int newYear)
        {
            var movie = movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
                throw new InvalidMovieIdException("Movie not found");
            movie.Name = newName;
            movie.Genre = newGenre;
            movie.Year = newYear;

            SerializeDeserialize.SerializeMovies(movies);
            return "Movie Updated Successfully";
        }
        public Movie FindMovieByID(int id)
        {
            var movie = movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
                throw new InvalidMovieIdException("Invalid ID");
            return movies.FirstOrDefault(m => m.MovieId == id);
            
        }
        public List<Movie> FindMovieByName(string name)
        {
            var movie = movies.FirstOrDefault(m => m.Name == name);
            if (movie == null)
                throw new InvalidMovieNameException("Invalid Movie Name");
            return movies.Where(m => m.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        public string RemoveMovieById(int id)
        {
            var movie = movies.FirstOrDefault(m => m.MovieId == id);
            if (movie == null)
            {
                throw new InvalidMovieIdException("Movie not found");
            }
            movies.Remove(movie);
            SerializeDeserialize.SerializeMovies(movies);
            return "Movie removed successfully";
        }
        public string ClearAllMovies()
        {
            movies.Clear();
            SerializeDeserialize.SerializeMovies(movies);
            return "All movies clear";
        }
    }
}