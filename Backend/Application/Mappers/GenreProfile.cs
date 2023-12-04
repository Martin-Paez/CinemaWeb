using Application.Dtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class GenreProfile : Profile
    {
        public GenreProfile() 
        {
            CreateMap<Genre, GenreResponse>();
        }
    }
}
