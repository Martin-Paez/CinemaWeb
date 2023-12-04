using Application.Dtos.Requests;
using Application.Dtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class ShowProfile : Profile
    {
        public ShowProfile() 
        {
            CreateMap<ShowRequiredParamsRequest, Show>();
            CreateMap<Show, DeepShowWithoutTicketsResponse>();
            CreateMap<Show, ShowScheduleResponse>();
        }
    }
}
