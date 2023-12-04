using Application.Interfaces.ICQRS.ICommands;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.CQRS.Commands
{
    public class MovieCommand : IMovieCommand
    {
        protected CinemaDbContext _dbContext;

        public MovieCommand(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> UpdateMovie(Movie response)
        {
            _dbContext.Update(response);
            return await _dbContext.SaveChangesAsync();
        }
    }
}
