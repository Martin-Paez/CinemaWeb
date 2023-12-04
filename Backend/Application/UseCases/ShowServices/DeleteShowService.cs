using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces.ICQRS.ICommands;
using Application.Interfaces.IUseCases.IShowServices;
using Application.Interfaces.IUseCases.ITicketServices;
using AutoMapper;

namespace Application.UseCases.ShowServices
{
    public class DeleteShowService : 
        IDeleteShowService
    {
        private readonly IShowCommands _showCommand;
        private readonly IGetShallowShowByIdService _getShowService;
        private readonly IGetTicketsAmountService _getTicketsAmountService;
        private readonly IMapper _mapper;

        public DeleteShowService(
            IShowCommands showCommands,
            IGetShallowShowByIdService getShowService,
            IMapper mapper,
            IGetTicketsAmountService getTicketsAmountService)
        {
            _showCommand = showCommands;
            _getShowService = getShowService;
            _mapper = mapper;
            _getTicketsAmountService = getTicketsAmountService;
        }

        public async Task<ShowScheduleResponse> DeleteShow(int showId)
        {
            var show = await _getShowService.GetEntityStrict(showId);
            var ticketsAmount = await _getTicketsAmountService.GetTicketsAmount(showId);
            if (ticketsAmount > 0)
                throw new ConflicException("La funcion tiene tickets vendidos, no puede eliminarse.");
            await _showCommand.DeleteShow(show);
            return _mapper.Map<ShowScheduleResponse>(show);
        }
    }
}
