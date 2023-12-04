using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.IQueries;
using Application.Interfaces.IUseCases.ITicketServices;
using AutoMapper;
using Domain.Entities;

namespace Application.UseCases.TicketServices
{
    public class GetTicketsAmountService :
        GetByIdService<Show>,
        IGetTicketsAmountService
    {
        private readonly IShowQueries _showQueries;

        public GetTicketsAmountService(
            IMapper mapper,
            IErrorMessageFactory errorFactory,
            IShowQueries showQueries
            )
            :base(mapper, errorFactory)
        {
            _showQueries = showQueries;
        }

        public async Task<int> GetTicketsAmount(int showId)
        {
            await GetEntityStrict(showId);
            return await _showQueries.GetTicketsAmount(showId);
        }

        protected override async Task<Show?> GetFromRepository(int id)
        {
            return await _showQueries.GetBySimpleIntegerPk(id);
        }
    }
}