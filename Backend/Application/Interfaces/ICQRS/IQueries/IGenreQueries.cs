using Application.Interfaces.CQRS.IQueries;
using Domain.Entities;

namespace Application.Interfaces.ICQRS.IQueries
{
    public interface IGenreQueries
        : ISimpleIntegerIdQuery<Genre>
    {
        public Task<List<Genre>> GetAll();
    }
}
