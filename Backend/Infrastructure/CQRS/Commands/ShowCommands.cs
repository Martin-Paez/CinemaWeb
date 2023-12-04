using Domain.Entities;
using Application.Interfaces.ICQRS.ICommands;
using Infrastructure.Persistence;

namespace Infrastructure.CQRS.Commands
{
    public class ShowCommands : IShowCommands
    {
        private readonly CinemaDbContext _dbContext;

        public ShowCommands(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddShow(Show show)
        {
            _dbContext.Add(show);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteShow(Show show)
        {
            _dbContext.Remove(show);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
