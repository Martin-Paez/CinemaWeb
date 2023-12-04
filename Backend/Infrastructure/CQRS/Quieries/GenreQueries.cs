using Application.Interfaces.ICQRS.IQueries;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CQRS.Quieries
{
    public class GenreQueries : 
        SimpleIntegerIdQuery<Genre>, 
        IGenreQueries
    {
        public GenreQueries(CinemaDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Genre>> GetAll()
        {
            return await _dbContext.Genres.ToListAsync();
        }
    }
}

