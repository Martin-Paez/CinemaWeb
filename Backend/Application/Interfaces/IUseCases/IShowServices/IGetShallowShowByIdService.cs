using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.IUseCases.IShowServices
{
    public interface IGetShallowShowByIdService :
        ISimpleIntegerIdService<Show, ShowScheduleResponse>
    {
    }
}
