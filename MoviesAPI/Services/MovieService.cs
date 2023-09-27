using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dto;
using MoviesAPI.Models;
using System.Collections.Generic;

namespace MoviesAPI.Services
{
    public class MovieService : IMovieService
    {
        AppDbContext context;

        public MovieService(AppDbContext _context)
        {
            context = _context;
        }

       
        public async Task<Movie> AddMovie(Movie movie)
        {
          await  context.AddAsync(movie);
            context.SaveChanges();
            return movie;
        }

        public Movie DeleteMovie(Movie movie)
        {
            context.Remove(movie);
            context.SaveChanges();
            return movie;
        }
        public Movie UpdateMovie(Movie movie)
        {
            context.Update(movie);

            context.SaveChanges();
            return movie;
        }
        public async Task<IEnumerable<Movie>> GetAll(byte genreid = 0)
        {
           var movies= await context.Movies.Where(g=>g.GenreId==genreid ||genreid==0)
                .OrderByDescending(g => g.Rate).Include(g => g.Genre).ToListAsync();

            return movies;
        }

        public async Task<Movie> GetMovie(int id)
        {
          return  await context.Movies.Include(g=>g.Genre).SingleOrDefaultAsync(g=>g.Id==id);
        }









        //public async Task<MovieDto> GetMovieById(int id)
        //{
        //    var movieDto = await context.Movies
        //        .Where(g => g.Id == id)
        //        .Select(g => new MovieDto
        //        {
        //            Title = g.Title,
        //            Year = g.Year,
        //            Rate = g.Rate,
        //            MyStorLine = g.MyStorLine,
        //            //  Poster = g.Poster, // If you have a Poster property in MovieDto
        //            GenreId = g.GenreId,
        //            GenreName = g.Genre.Name
        //        })
        //        .SingleOrDefaultAsync();

        //    return movieDto;
        //}

       


    }
}
