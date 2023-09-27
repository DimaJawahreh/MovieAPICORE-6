using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dto;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieService movieService;
        IGenreService  genreService;
        IMapper mapper;

        new List<string> allowedextension = new List<string> { ".jpg",".png"};

        long allowedlengthpostersize = 1048576;


        public MovieController(IMovieService _movieService,IGenreService _genreService,IMapper _mapper) 
        {
            genreService = _genreService;
            movieService = _movieService;
            mapper = _mapper;


        }
      
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()//done to service
        {
            var movies=   await movieService.GetAll();
            var data=mapper.Map<IEnumerable<MovieDto>>(movies);
            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)//done to service
        {
            var movie = await movieService.GetMovie(id);
            if (movie == null)
                return NotFound();

            var data=mapper.Map<MovieDto>(movie);


            return Ok(data);
        }


        [HttpGet("GetByGenraId")]
        public async Task<IActionResult> GetByGenraId(byte genreid)
        {
            var movies = await movieService.GetAll(genreid);
            var data =mapper.Map<IEnumerable<MovieDto>>(movies);
            return Ok(data);
        }
        

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm]MovieDto  movieDto)
        {

            if (movieDto.Poster == null)
                return BadRequest("Poster is required");

            if (!allowedextension.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                return BadRequest("just jpg and png ");

            if(allowedlengthpostersize> movieDto.Poster.Length)
                return BadRequest("Max Allowed Length for Poster Size is 1MG ");


            var isValidGenre = await genreService.GetById(movieDto.GenreId);//or create a new method in genre
                                                                            //service that return a bool
                                                                            //context.Genres.AnyAsync(g => g.Id == movieDto.GenreId);
            if (isValidGenre==null) 
             return BadRequest("Invalid genra id");


            using var memorystream=new MemoryStream();
            await movieDto.Poster.CopyToAsync(memorystream);
            var movie =mapper.Map<Movie>(movieDto);
            movie.Poster = memorystream.ToArray();
            //var movie = new Movie 
            //{ 
            //MyStorLine = movieDto.MyStorLine,
            //Title = movieDto.Title,
            //Year = movieDto.Year,
            //Rate = movieDto.Rate,
            //GenreId= movieDto.GenreId,
            //Poster=memorystream.ToArray()



            //};

            await movieService.AddMovie(movie);
            return Ok(movie);
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto movieDto)
        {
            var movie = await movieService.GetMovie(id);
            if (movie == null) return NotFound($"no movie was found with id {id}");



            var isValidGenre = await genreService.GetById(movieDto.GenreId);
            if (isValidGenre==null)
                return BadRequest("Invalid genra id");



            if(movieDto.Poster != null)
            {
                if (!allowedextension.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                    return BadRequest("just jpg and png ");

                if (allowedlengthpostersize > movieDto.Poster.Length)
                    return BadRequest("Max Allowed Length for Poster Size is 1MG ");


                using var memoryStream = new MemoryStream();
                await movieDto.Poster.CopyToAsync(memoryStream);
                movie.Poster = memoryStream.ToArray();

            }

            movie.MyStorLine=movieDto.MyStorLine;
            movie.Year=movieDto.Year;
            movie.GenreId=movieDto.GenreId;
            movie.Rate=movieDto.Rate;
            movie.Title=movieDto.Title;

            movieService.UpdateMovie(movie);
            return Ok(movie);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie=await movieService.GetMovie(id);

            if (movie == null) return NotFound();
            movieService.DeleteMovie(movie);
            return Ok(movie);
        }//done

    }
}
