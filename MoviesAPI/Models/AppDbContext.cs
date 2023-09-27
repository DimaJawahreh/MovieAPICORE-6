using Microsoft.EntityFrameworkCore;

namespace MoviesAPI.Models
{
    public class AppDbContext:DbContext
    {
        private IConfiguration configuration;
        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration _configuration) :base(options)
        { 
            configuration = _configuration;
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("defultconnection"));
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }


    }
}
