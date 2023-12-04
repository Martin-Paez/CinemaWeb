using Application.Dtos.Responses;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IGenreServices;
using AutoMapper;

namespace Application.UseCases.GenreServices
{
    public class GetAllGenreService : IGetAllGenreService
    {
        private IGenreQueries _genreQuery;
        private IMapper _mapper;

        public GetAllGenreService(
            IGenreQueries genreQuery, 
            IMapper mapper
            )
        {
            _genreQuery = genreQuery;
            _mapper = mapper;
        }

        public async Task<List<GenreResponse>> GetAll()
        {
            var genres = await _genreQuery.GetAll();
            return _mapper.Map<List<GenreResponse>>( genres );
        }
    }
}
