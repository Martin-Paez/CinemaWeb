using Application.Dtos.Requests;
using Application.Dtos.Responses;

namespace Application.Interfaces.IUseCases.IMovieServices
{
    public interface IUpdateMovieService
    {
        public Task<MovieResponse> UpdateMovie(
            int movieId,
            MovieOptionalParamsRequest movieRequest);
    }
}
