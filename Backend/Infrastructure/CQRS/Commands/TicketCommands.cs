using Application.Dtos;
using Application.Interfaces.CQRS.ICommands;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.CQRS.Commands
{
    public class TicketCommands : ITicketCommands
    {
        private readonly CinemaDbContext _dbContext;

        public TicketCommands(CinemaDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IList<Ticket>> AddTickets(
            TicketTemplate ticketTemplate, 
            int amount
            )
        {
            var tickets = new List<Ticket>();
            for (int i = 0; i < amount; i++)
            {
                var t = CreateTicket(ticketTemplate);
                _dbContext.Tickets.Add(t);
                tickets.Add(t);
            }
            await _dbContext.SaveChangesAsync();
            return tickets;
        }

        private Ticket CreateTicket(TicketTemplate ticketTemplate)
        {
            return new Ticket
            {
                ShowId = ticketTemplate.ShowId,
                User = ticketTemplate.User,
                TicketId = Guid.NewGuid()
            };
        }
    }
}
