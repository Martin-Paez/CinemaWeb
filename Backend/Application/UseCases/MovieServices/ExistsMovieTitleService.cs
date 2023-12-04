using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IMovieServices;

namespace Application.UseCases.MovieServices
{
    public class ExistsMovieTitleService : IExistsMovieTitleService
    {
        private readonly IMovieQueries _movieQueries;

        public ExistsMovieTitleService(
            IMovieQueries movieQueries
            )
        {
            _movieQueries = movieQueries;
        }

        public async Task<bool> ExistMovieTitle(string title)
        {
            if(string.IsNullOrEmpty(title)) 
                return true;
            return await _movieQueries.ExistMovieTitle(title);
        }
    }
}