using Application.Dtos.Responses;

namespace Application.Interfaces.IUseCases.IShowServices
{
    public interface IGetShowBySpecificationService
    {
        public Task<IList<DeepShowWithoutTicketsResponse>> GetBy(
            string? date,
            string? title,
            int? genre
        );
    }
}
