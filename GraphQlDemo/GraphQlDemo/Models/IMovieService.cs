using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQlDemo.Models
{
    public interface IMovieService
    {
        IQueryable<Movie> GetAll();
        Movie Create(CreateMovieInput inputMovie);
        Movie Delete(DeleteMovieInput inputMovie);
    }

    public class MovieService : IMovieService
    {
        private readonly MovieDbContext _context;

        public MovieService(MovieDbContext context)
        {
            _context = context;
        }

        public IQueryable<Movie> GetAll()
        {
            return _context.Movies;
        }

        public Movie Create(CreateMovieInput inputMovie)
        {
            var Movie = _context.Movies.Add(
                new Movie { Title = inputMovie.Title, Price = inputMovie.Price });
            _context.SaveChanges();
            return Movie.Entity;
        }

        public Movie Delete(DeleteMovieInput inputMovie)
        {
            var Movie = _context.Movies.Find(inputMovie.Id);
            _context.Movies.Remove(Movie);
            _context.SaveChanges();
            return Movie;
        }
    }


    public class DeleteMovieInput
    {
        public long Id { get; set; }
    }

    public class CreateMovieInput
    {
        public string Title { get; set; }
        public double Price { get; set; }
    }
}