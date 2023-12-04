using Application.Interfaces.CQRS.IQueries;
using Domain.Entities;

namespace Application.Interfaces.ICQRS.IQueries
{
    public interface IMovieQueries : 
        ISimpleIntegerIdQuery<Movie>
    {
        public Task<Movie?> GetDeep(int id);
        public Task<bool> ExistMovieTitle(string title);
    }
}
