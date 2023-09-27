using MoviesAPI.Dto;

namespace MoviesAPI.Services
{
    public interface IMovieService
    {
         Task<IEnumerable<Movie>> GetAll(byte genreid=0);//byte genreid=0 if i didnt send any value ,the defult is 0


        //Task<MovieDto> GetMovieById(int id);

         Movie   DeleteMovie(Movie movie);
         Task<Movie> GetMovie(int id);
        Task<Movie> AddMovie(Movie movie );
        Movie UpdateMovie(Movie movie);

    }
}
