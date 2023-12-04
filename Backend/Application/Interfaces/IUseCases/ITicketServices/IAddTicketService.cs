using Application.Dtos.Requests;
using Application.Dtos.Responses;

namespace Application.Interfaces.IUseCases.ITicketServices
{
    public interface IAddTicketService
    {
        public Task<AddTicketsResponse> AddTicket(
            int funcitonId,
            TicketRequiredParamsRequest ticketRequest
        );
    }
}
