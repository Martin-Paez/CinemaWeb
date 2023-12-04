using Application.Dtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ScreenProfile : Profile
    {
        public ScreenProfile() 
        {
            CreateMap<Screen, ScreenResponse>();
        }
    }
}