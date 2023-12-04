using Application.Dtos;
using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces.CQRS.ICommands;
using Application.Interfaces.IError;
using Application.Interfaces.IUseCases.IShowServices;
using Application.Interfaces.IUseCases.ITicketServices;
using Application.Validators;
using AutoMapper;
using Domain.Entities;
using System.Text;

namespace Application.UseCases.TicketServices
{
    public class AddTicketsService : IAddTicketService
    {
        private readonly ITicketCommands _ticketCommands;
        private readonly IGetTicketsAmountService _ticketsAmountService;
        private readonly IGetDeepShowByIdWithoutTicketsService _getDeepShowByIdWithoutTicketsService;
        private readonly IErrorMessageFactory _errorFactory;
        private readonly IMapper _mapper;

        public AddTicketsService(
            IMapper mapper,
            ITicketCommands ticketCommands,
            IGetTicketsAmountService ticketsAmountService,
            IErrorMessageFactory errorFactory,
            IGetDeepShowByIdWithoutTicketsService getDeepShowByIdWithoutTicketsService)
        {
            _mapper = mapper;
            _ticketCommands = ticketCommands;
            _ticketsAmountService = ticketsAmountService;
            _errorFactory = errorFactory;
            _getDeepShowByIdWithoutTicketsService = getDeepShowByIdWithoutTicketsService;
        }

        public async Task<AddTicketsResponse> AddTicket(
            int showId,
            TicketRequiredParamsRequest ticketRequest
            )
        {
            var show = await ValidateRequest(showId, ticketRequest);
            // Actualizacion : Para el TP se lanza 400 en caso de falta de 
            // capacidad de las salas del cine. Se lanza la excepcion por 
            // separado a las demas excepciones que se mapearan a un 400,
            // para destacar que deberia ser un 409 o 422.
            var tickets = await AddTickets(showId, ticketRequest);
            return CreateResponse(
                tickets,
                ticketRequest.User,
                show
            );
        }

        private async Task<DeepShowWithoutTicketsResponse> ValidateRequest(
            int showId,
            TicketRequiredParamsRequest ticketRequest
            )
        {
            ValidateRequestWithoutDataBaseAccess(showId, ticketRequest);
            var show = await _getDeepShowByIdWithoutTicketsService
                .GetResponseDto(showId);
            await ValidateRoomCapacity( 
                show,
                ticketRequest.Amount
            );
            return show!;
        }

        private void ValidateRequestWithoutDataBaseAccess(
            int showId,
            TicketRequiredParamsRequest ticketRequest
            )
        {
            var errors = new StringBuilder();
            if (showId < 1)
                errors.Append(
                    _errorFactory.InvalidIdFormat<Show>()
                );
            ValidateTicketRequest(ticketRequest, errors);
            if (errors.Length > 0)
                throw new InvalidArgumentsException(errors.ToString());
        }

        private void ValidateTicketRequest(
            TicketRequiredParamsRequest ticketRequest,
            StringBuilder errorBuilder
            )
        {
            var validator = new DbFieldValidator<TicketRequiredParamsRequest>(_errorFactory);
            validator.AddVarcharRule(
                t => t.User,
                new TicketConstraints().userLength,
                "usuario"
            );
            var result = validator.Validate(ticketRequest);
            if (!result.IsValid)
                foreach (var err in result.Errors)
                    errorBuilder.Append(err.ErrorMessage);
            if (ticketRequest.Amount < 1)
                errorBuilder.Append(_errorFactory.InvalidTicketsAmount());
        }

        private async Task ValidateRoomCapacity(
            DeepShowWithoutTicketsResponse show,
            int amountRequired
            )
        {
            var sales = 0;
            try
            {
                sales = await _ticketsAmountService
                    .GetTicketsAmount(show.Id);
            }
            catch { }
            if (sales + amountRequired > show.ScreenNav.Capacity)
                // Actualizacion: Deberia responderse 409 (para el TP 400)
                throw new InvalidArgumentsException(
                    _errorFactory.InsuficientSeats(
                        show.ScreenNav.Capacity - sales
                    )
                );
        }

        private async Task<IList<Ticket>> AddTickets(
            int showId,
            TicketRequiredParamsRequest ticketRequest
            )
        {
            var ticketTemplate = new TicketTemplate
            {
                ShowId = showId,
                User = ticketRequest.User
            };
            return await _ticketCommands.AddTickets(
                ticketTemplate,
                ticketRequest.Amount
            );
        }

        private AddTicketsResponse CreateResponse(
            IList<Ticket> tickets,
            string userName,
            DeepShowWithoutTicketsResponse showResponse
            )
        {
            var ticketResponses = new List<TicketIdDto>();
            foreach (var t in tickets)
            {
                var r = _mapper.Map<Ticket, TicketIdDto>(t);
                ticketResponses.Add(r);
            }
            return new AddTicketsResponse
            {
                Tickets = ticketResponses,
                Show = showResponse,
                User = userName
            };
        }
    }
}
