using Application.Dtos.Requests;
using Application.Dtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class MovieProfile : Profile
    {
        public MovieProfile() 
        {
            CreateMap<MovieOptionalParamsRequest, Movie>()
                .ForAllMembers(opt => opt.Condition(
                    (src, dest, srcMember) => srcMember != null)
                );
            CreateMap<Movie, LightMovieResponse>();
            CreateMap<MovieOptionalParamsRequest, Movie>();
            CreateMap<Movie, MovieResponse>();
        }
    }
}