using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.CQRS.ICommands
{
    public interface ITicketCommands
    {
        public Task<IList<Ticket>> AddTickets(TicketTemplate ticket, int amount);
    }
}
