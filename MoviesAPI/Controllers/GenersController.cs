using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dto;
using MoviesAPI.Services;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenersController : ControllerBase
    {

        IGenreService genreService;

        public GenersController(IGenreService _genreService)
        {
            genreService = _genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genes = await genreService.GetAll();
            return Ok(genes);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(CreateGenredto createGenredto)
        {
            var Genre = new Genre();
            Genre.Name = createGenredto.Name;
            await genreService.Add(Genre);//no need to set context.genre.add (رح يفهم انه بده يعمل ادد عال جنرا من خلال النوع اللي وصله)
            return Ok(Genre);

        }
        [HttpPut("{id}")]
        //api/Genre/id
       // [FromBody] wich means that the 
        public async Task<IActionResult> UpdateGenre(byte id, [FromBody] CreateGenredto dto)
        {
            var genra = await genreService.GetById(id);
            if (genra==null) 
                return NotFound($"no genra was found id {id}");

            genra.Name = dto.Name;
            genreService.Update(genra);
                return Ok(genra);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await genreService.GetById(id);
            if (genre == null)
                return NotFound($"no genra was found id {id}");

            genreService.Delete(genre);
            return Ok(genre);
        }
    }
}
