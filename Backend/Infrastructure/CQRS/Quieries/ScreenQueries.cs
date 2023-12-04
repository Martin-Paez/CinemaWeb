using Application.Interfaces.ICQRS.IQueries;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.CQRS.Quieries
{
    public class ScreenQueries : 
        SimpleIntegerIdQuery<Screen>, 
        IScreenQueries
    {
        public ScreenQueries(CinemaDbContext dbContext) : 
            base(dbContext)
        {

        }
    }
}