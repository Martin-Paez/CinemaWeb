using Application.Dtos.Responses;
using Domain.Entities;

namespace Application.Interfaces.IUseCases.IRoomServices
{
    public interface IGetScreenByIdService : 
        ISimpleIntegerIdService<Screen, ScreenResponse>
    {
    }
}
