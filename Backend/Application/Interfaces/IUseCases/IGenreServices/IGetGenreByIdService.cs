using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.IUseCases.IGenreServices
{
    public interface IGetGenreByIdService :
        ISimpleIntegerIdService<Genre, GenreResponse>
    {
    }
}
