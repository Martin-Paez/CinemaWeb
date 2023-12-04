using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.IUseCases.IMovieServices
{
    public interface IGetMovieByIdService :
        ISimpleIntegerIdService<Movie, MovieResponse>
    {
    }
}
