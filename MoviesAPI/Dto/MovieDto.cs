namespace MoviesAPI.Dto
{
    public class MovieDto
    {
        [MaxLength(250)]
        public string Title { get; set; }

        public int Year { get; set; }

        public double Rate { get; set; }
        [MaxLength(2500)]
        public string MyStorLine { get; set; }
        public IFormFile? Poster { get; set; }//photo ?:nullable

        public byte GenreId { get; set; }
        public string GenreName { get;  set; }
    }
}
