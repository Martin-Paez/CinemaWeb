using Domain.Entities;

namespace Application.Interfaces.IUseCases.IShowServices
{
    public interface ICheckOverlappingShowsService
    {
        public Task<bool> IsOverlapping(
            Show show,
            int duration
        );
    }
}