using Application.Dtos.Requests;
using Application.Dtos.Responses;

namespace Application.Interfaces.IUseCases.IShowServices
{
    public interface IAddShowService
    {
        public Task<DeepShowWithoutTicketsResponse> AddShow(ShowRequiredParamsRequest request, int duration);
    }
}
