using Application.Dtos.Responses;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class TicketProfile : Profile
    {
        public TicketProfile() 
        {
            CreateMap<Ticket, TicketIdDto>();
        }
    }
}
