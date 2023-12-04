using Application.Dtos.Responses;
using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IGenreServices;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.GenreServices
{
    public class GetGenreByIdService : 
        GetByIdService<Genre, GenreResponse>,
        IGetGenreByIdService
    {
        private readonly IGenreQueries _genreQueries;

        public GetGenreByIdService(
            IGenreQueries genreQueries,
            IMapper mapper,
            IErrorMessageFactory errorMessageFactory
            )
            :base(mapper, errorMessageFactory)
        {
            _genreQueries = genreQueries;
        }

        protected override async Task<Genre?> GetFromRepository(int id)
        {
            return await _genreQueries.GetBySimpleIntegerPk(id);
        }

        protected override Task<bool> ExistsInRepository(int id)
        {
            return _genreQueries.Exists(id);
        }
    }
}
