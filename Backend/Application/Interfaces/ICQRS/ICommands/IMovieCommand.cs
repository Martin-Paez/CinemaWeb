using Domain.Entities;

namespace Application.Interfaces.ICQRS.ICommands
{
    public interface IMovieCommand
    {
        public Task<int> UpdateMovie(Movie response);
    }
}
