using Application.Dtos.Responses;
using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.IShowServices;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.ShowServices
{
    public class GetDeepShowByIdWithoutTicketsService :
        GetByIdService<Show, DeepShowWithoutTicketsResponse>,
        IGetDeepShowByIdWithoutTicketsService
    {
        private readonly IShowQueries _showQueries;

        public GetDeepShowByIdWithoutTicketsService(
            IShowQueries showQueries,
            IMapper mapper,
            IErrorMessageFactory errorMessageFactory
            )
            : base(mapper, errorMessageFactory)
        {
            _showQueries = showQueries;
        }

        protected override async Task<Show?> GetFromRepository(int id)
        {
            return await _showQueries.GetDeepWithoutTickets(id);
        }

        protected override Task<bool> ExistsInRepository(int id)
        {
            return _showQueries.Exists(id);
        }
    }
}
