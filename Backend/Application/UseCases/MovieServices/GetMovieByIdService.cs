using Application.Dtos.Responses;
using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IMovieServices;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.MovieServices
{
    public class GetMovieByIdService : 
        GetByIdService<Movie, MovieResponse>,
        IGetMovieByIdService
    {
        private readonly IMovieQueries _movieQuery;

        public GetMovieByIdService(
            IMapper movieMapper,
            IMovieQueries movieQueries,
            IErrorMessageFactory errorMessageFactory
            )
            :base( movieMapper, errorMessageFactory )
        {
            _movieQuery = movieQueries;
        }

        protected override async Task<Movie?> GetFromRepository(int id)
        {
            return await _movieQuery.GetDeep(id);
        }

        protected override Task<bool> ExistsInRepository(int id)
        {
            return _movieQuery.Exists(id);
        }
    }
}