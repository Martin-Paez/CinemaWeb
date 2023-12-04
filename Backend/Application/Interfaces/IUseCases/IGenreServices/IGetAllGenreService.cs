using Application.Dtos.Responses;

namespace Application.Interfaces.IUseCases.IGenreServices
{
    public interface IGetAllGenreService
    {
        public Task<List<GenreResponse>> GetAll();
    }
}