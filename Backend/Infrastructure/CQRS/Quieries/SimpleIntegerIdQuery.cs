using Application.Interfaces.CQRS.IQueries;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.CQRS.Quieries
{
    public abstract class SimpleIntegerIdQuery<Entity> :
        ISimpleIntegerIdQuery<Entity>
        where Entity : SimpleIntegerIdEntity
    {
        protected readonly CinemaDbContext _dbContext;
        protected SimpleIntegerIdQuery(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Entity?> GetBySimpleIntegerPk(int id)
        {
            return await _dbContext.Set<Entity>().FindAsync(id);
        }

        public async Task<bool> Exists(int id)
        {
            return await _dbContext
                .Set<Entity>()
                .AnyAsync(e => e.Id == id);
        }
    }
}
