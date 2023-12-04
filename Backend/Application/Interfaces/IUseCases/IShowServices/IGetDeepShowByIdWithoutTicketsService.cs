using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.IUseCases.IShowServices
{
    public interface IGetDeepShowByIdWithoutTicketsService :
        ISimpleIntegerIdService<Show, DeepShowWithoutTicketsResponse>
    {

    }
}
