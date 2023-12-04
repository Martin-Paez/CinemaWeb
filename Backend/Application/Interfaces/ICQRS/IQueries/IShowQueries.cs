using Application.Interfaces.CQRS.IQueries;
using Application.SpecificationPattern;
using Domain.Entities;

namespace Application.Interfaces.ICQRS.IQueries
{
    public interface IShowQueries
        : ISimpleIntegerIdQuery<Show>
    {
        public Task<Show?> GetDeepWithoutTickets(int showId);
        public Task<int> GetTicketsAmount(int showId);
        public ISpecification<Show> ByDay(DateTime date);
        public ISpecification<Show> ByTitle(string movieTitle);
        public ISpecification<Show> ByGenre(int genreId);
        public Task<IList<Show>> GetDeepWithoutTicketsBy(ISpecification<Show> spec);
        public Task<IList<Show>> GetDeepWithoutTickets();
        public Task<List<Show>> GetShowsByScreenInTheSameDayAndNextDayAs(Show show);
        public Task<List<Show>> GetShowsByScreenInTheSameDayAs(Show show);
        public Task<List<Show>> GetShowsByScreenInTheSameDayAndPreviousDayAs(Show show);
    }
}
