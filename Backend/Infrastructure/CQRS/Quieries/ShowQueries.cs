using Application.Interfaces.ICQRS.IQueries;
using Application.SpecificationPattern;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.CQRS.Quieries
{
    public class ShowQueries : 
        SimpleIntegerIdQuery<Show>, 
        IShowQueries
    {
        public ShowQueries(CinemaDbContext dbContext) 
            : base(dbContext)
        {
        }

        public async Task<int> GetTicketsAmount(int showId)
        {
            return await _dbContext.Set<Show>()
                .Include(s => s.Tickets)
                .Where(s => s.Id == showId)
                .Select(s => s.Tickets!.Count)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retorna todos los registros de la tabla Funciones con los 
        /// registros relacionados por FK.
        /// </summary>
        public async Task<IList<Show>> GetDeepWithoutTickets()
        {
            return await DeepQueryWithoutTickets().ToListAsync();
        }

        public async Task<IList<Show>> GetDeepWithoutTicketsBy(
            ISpecification<Show> specification
            )
        {
            return await DeepQueryWithoutTickets()
                .Where(specification.ToExpression())
                .ToListAsync();
        }

        public async Task<Show?> GetDeepWithoutTickets(int showId)
        {
            return await DeepQueryWithoutTickets()
                .Where(m => m.Id == showId)
                .FirstOrDefaultAsync();
        }

        private IIncludableQueryable<Show, Screen?> DeepQueryWithoutTickets()
        {
            return _dbContext.Set<Show>()
               .Include(m => m.MovieNav)
               .ThenInclude(x => x.GenreNav)
               .Include(m => m.ScreenNav);
        }

        public ISpecification<Show> ByDay(DateTime date)
        {
            return new EFSpecification<Show>((s) =>

                s.Date.CompareTo(date) == 0
            );
        }

        public ISpecification<Show> ByTitle(string movieTitle)
        {
            return new EFSpecification<Show>((s) =>
            
                movieTitle.Contains(s.MovieNav!.Title) ||
                s.MovieNav!.Title.Contains(movieTitle)
            );
        }

        public ISpecification<Show> ByGenre(int genreId)
        {
            return new EFSpecification<Show>((s) =>
            
                genreId == s.MovieNav!.Genre
            );
        }

        public async Task<List<Show>> GetShowsByScreenInTheSameDayAndNextDayAs(
            Show show
            )
        {
            return await _dbContext
                .Set<Show>()
                .Where(s =>
                    (s.Date == show.Date || s.Date == show.Date.AddDays(1)) &&
                    s.ScreenId == show.ScreenId)
                .ToListAsync();
        }

        public async Task<List<Show>> GetShowsByScreenInTheSameDayAs(
            Show show
            )
        {
            return await _dbContext
                .Set<Show>()
                .Where(s =>
                    s.Date == show.Date &&
                    s.ScreenId == show.ScreenId)
                .ToListAsync();
        }

        public async Task<List<Show>> GetShowsByScreenInTheSameDayAndPreviousDayAs(
            Show show
            )
        {
            return await _dbContext
                .Set<Show>()
                .Where(s =>
                    (s.Date == show.Date || s.Date == show.Date.AddDays(-1)) &&
                    s.ScreenId == show.ScreenId)
                .ToListAsync();
        }
    }
}