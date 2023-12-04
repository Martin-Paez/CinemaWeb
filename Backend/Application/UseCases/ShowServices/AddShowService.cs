using Application.Dtos.Requests;
using Application.Dtos.Responses;
using Application.Exceptions;
using Application.Interfaces.IError;
using Application.Interfaces.ICQRS.ICommands;
using Application.Interfaces.IUseCases.IRoomServices;
using Application.Interfaces.IUseCases.IShowServices;
using Application.Validators;
using Domain.Entities;
using System.Text;
using Application.Interfaces.IUseCases.IMovieServices;

namespace Application.UseCases.ShowServices
{
    /// <summary>
    /// Serivicio encargado de insertar un registro en la tabla Funciones
    /// </summary>
    public class AddShowService : IAddShowService
    {
        private readonly IShowCommands _showCommand;
        private readonly IGetDeepShowByIdWithoutTicketsService _getDeepShowByIdWithoutTicketsService;
        private readonly IGetScreenByIdService _getScreenService;
        private readonly IGetMovieByIdService _getMovieByIdService;
        private readonly ICheckOverlappingShowsService _checkOverlappingShowsService;
        private readonly IErrorMessageFactory _errorMsgFactory;

        public AddShowService(
            IShowCommands showCommand,
            IGetScreenByIdService getRooms,
            ICheckOverlappingShowsService checkOverlappingShowsService,
            IErrorMessageFactory errorMessageFactory,
            IGetDeepShowByIdWithoutTicketsService getDeepShowByIdWithoutTicketsService,
            IGetMovieByIdService getMovieByIdService)
        {
            _showCommand = showCommand;
            _getScreenService = getRooms;
            _checkOverlappingShowsService = checkOverlappingShowsService;
            _errorMsgFactory = errorMessageFactory;
            _getDeepShowByIdWithoutTicketsService = getDeepShowByIdWithoutTicketsService;
            _getMovieByIdService = getMovieByIdService;
        }

        public async Task<DeepShowWithoutTicketsResponse> AddShow(
            ShowRequiredParamsRequest showRequest,
            int duration
            )
        {
            var show = await ValidateRequest(showRequest, duration);
            await _showCommand.AddShow(show);
            return await _getDeepShowByIdWithoutTicketsService.GetResponseDto(
                (int)show!.Id!
            );
        }

        private async Task<Show> ValidateRequest(
            ShowRequiredParamsRequest showRequest,
            int duration
            )
        {
            var errorBuilder = new StringBuilder();
            await ValidateRoomAndMovieIdFormat(showRequest, errorBuilder);
            var validator = new DateAndTimeValidator(_errorMsgFactory);
            DateTime? date = validator.ValidateDate(showRequest.Date, errorBuilder);
            TimeSpan? time = validator.ValidateTime(showRequest.Schedule, errorBuilder);
            if (errorBuilder.Length > 0)
                throw new InvalidArgumentsException(errorBuilder.ToString());
            return await ValidateSchedule(
                showRequest, 
                (DateTime) date!, 
                (TimeSpan) time!,
                duration
            );
        }

        private async Task<Show> ValidateSchedule(
            ShowRequiredParamsRequest showRequest,
            DateTime date,
            TimeSpan time,
            int duration
            )
        {
            var errorBuilder = new StringBuilder();
            int dateExpiredComparision = CheckExpiredDate(date!, errorBuilder);
            if (dateExpiredComparision == 0)
                CheckExpiredTime(time!, errorBuilder);
            Show show = await CheckOverlapping(
                showRequest,
                errorBuilder,
                date,
                time,
                duration
            );
            if (errorBuilder.Length > 0)
                throw new ConflicException(errorBuilder.ToString());
            return show;
        }

        private async Task ValidateRoomAndMovieIdFormat(
            ShowRequiredParamsRequest showRequest,
            StringBuilder errorBuilder
            )
        {
            await _getScreenService.Exists(
                showRequest.ScreenId,
                errorBuilder
             );
            await _getMovieByIdService.Exists(
                showRequest.MovieId,
                errorBuilder
            );
        }

        public int CheckExpiredDate(
            DateTime date,
            StringBuilder errors
            )
        {
            int dateComp = date.CompareTo(DateTime.Now.Date);
            if (dateComp < 0)
                errors.Append(_errorMsgFactory.ElapsedDate());
            return dateComp;
        }

        public void CheckExpiredTime(
            TimeSpan time,
            StringBuilder errors
            )
        {
            DateTime now = DateTime.Now;
            TimeSpan currentTime = new TimeSpan(
                now.Hour,
                now.Minute,
                0
            );
            var formatedTime = TimeSpan.FromTicks(time.Ticks);
            if (formatedTime.CompareTo(currentTime) <= 0)
                errors.Append(_errorMsgFactory.ElapsedTime());
        }

        public async Task<Show> CheckOverlapping(
            ShowRequiredParamsRequest showRequest,
            StringBuilder errorBuilder,
            DateTime date,
            TimeSpan time,
            int duration
            )
        {
            var show = new Show
            {
                Schedule = time!,
                Date = date!,
                ScreenId = showRequest.ScreenId,
                MovieId = showRequest.MovieId,
            };
            var overlappings = await _checkOverlappingShowsService
                .IsOverlapping(show, duration);
            if (overlappings)
                errorBuilder.Append(
                    _errorMsgFactory.OverlappingShows()
                 );
            return show;
        }
    }
}
