using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Services
{
    public class GenreService : IGenreService
    {
        AppDbContext context;

        public GenreService(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<Genre> Add(Genre genre)
        {

            await context.AddAsync(genre);//no need to set context.genre.add 
            context.SaveChanges();
            return genre;

        }

        public Genre Delete(Genre genre)
        {
            context.Remove(genre);
            context.SaveChanges();
            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await context.Genres.OrderBy(o => o.Name).ToListAsync();//await with async
            
        }

        public async Task<Genre> GetById(byte id)
        {
          return  await context.Genres.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Genre  Update(Genre genre)
        {
            
            context.Update(genre);
            context.SaveChanges();
            return genre;
        }
    }
}
