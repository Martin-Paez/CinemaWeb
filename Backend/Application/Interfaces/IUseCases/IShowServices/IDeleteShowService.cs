using Application.Dtos.Responses;

namespace Application.Interfaces.IUseCases.IShowServices
{
    public interface IDeleteShowService
    {
        public Task<ShowScheduleResponse> DeleteShow(int showId);
    }
}
