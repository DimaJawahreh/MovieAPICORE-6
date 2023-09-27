using AutoMapper;
using MoviesAPI.Dto;

namespace MoviesAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MovieDto>()
               .ForMember(dest => dest.Poster, opt => opt.Ignore());
            
            CreateMap<MovieDto, Movie>().ForMember(src => src.Poster, opt => opt.Ignore());
            //this is mean i ignore poster from mapping,i mapped it manullay
        }



    }
}
