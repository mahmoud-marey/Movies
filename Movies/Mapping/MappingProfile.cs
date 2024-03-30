namespace Movies.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        //Genres
        CreateMap<Genre, GenreDetailsDto>();
        CreateMap<GenreDto,Genre>();

        //Movies
        CreateMap<Movie, MovieDetailsDto>();
        CreateMap<MovieCreateDto, Movie>()
            .ForMember(dest => dest.Poster ,opt => opt.MapFrom(src => Converter.IFormFileToByteArray(src.Poster)));
        CreateMap<MovieUpdateDto, Movie>()
            .ForMember(dest => dest.Poster, opt => opt.Ignore());
    }
    
}
