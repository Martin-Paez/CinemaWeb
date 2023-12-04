using Application.Interfaces.ICQRS.IQueries;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CQRS.Quieries
{
    public class MovieQueries :
        SimpleIntegerIdQuery<Movie>,
        IMovieQueries
    {
        public MovieQueries(CinemaDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<Movie?> GetDeep(int id)
        {
            return await _dbContext
                .Set<Movie>()
                .Where(m => m.Id == id)
                .Include(m => m.GenreNav)
                .Include(m => m.Shows)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistMovieTitle(string title)
        {
            return await _dbContext
                .Set<Movie>()
                .AnyAsync(m => m.Title == title);
        }
    }
}