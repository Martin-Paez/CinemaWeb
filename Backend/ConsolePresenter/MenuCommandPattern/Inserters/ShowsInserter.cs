using Application.Dto.Request;
using Application.Dto.Response.AddResponse;
using Application.Interfaces.IServices.IMovie;
using Application.Interfaces.IServices.IRoom;
using Application.Interfaces.IServices.IShow;
using ConsolePresenter.MenuCommandPattern.BasicPrinters.ConcreteBasicPrinters;
using ConsoleView;
using Domain.Entities;
using Vista.Command_Pattern;

namespace ConsolePresenter.MenuCommandPattern.Inserters
{
    public class ShowsInserter : ICommand
    {
        private IAddShowService _addShow;
        private IGetAllMoviesService _getMovies;
        private IGetAllRoomsService _getRooms;
        private IEnhacedConsole _cmd;

        public ShowsInserter(
            IEnhacedConsole cmd,
            IAddShowService addShow,
            IGetAllMoviesService getMovies,
            IGetAllRoomsService getRooms
            )
        {
            _addShow = addShow;
            _cmd = cmd;
            _getMovies = getMovies;
            _getRooms = getRooms;
        }

        public bool Execute()
        {
            var movies = new MoviesPrinter(_cmd, _getMovies);
            var rooms = new RoomsPrinter(_cmd, _getRooms);
            bool response;
            var showResponse = new ShowRequest()
            {
                Time = _cmd.ReadTime(),
                Date = _cmd.ReadDate(),
                MovieId = movies.SelectOne().MovieId,
                RoomId = rooms.SelectOne().RoomId
            };
            response = InsertOnlyOne(showResponse);
            return response;
        }

        /// <exception cref="CanceledEntry"></exception>
        public bool InsertOnlyOne(ShowRequest request)
        {
            var requests = new List<ShowRequest>() { request };
            var response = _addShow.Add(requests);
            if (response.refuseds.Count > 0)
                return SolveProblem(response.refuseds[0], request);
            return true;
        }

        /// <exception cref="CanceledEntry"></exception>
        public bool SolveProblem(Refused<ShowRequest> refused, ShowRequest request)
        {
            foreach (var error in refused.Errors)
            {
                _cmd.PrintRed("\n" + error.Message + "\n");
                switch (error.Code)
                {
                    case 11001:
                        request.Date = _cmd.ReadDate(); 
                        break;
                    case 11002:
                        request.Time = _cmd.ReadTime(); 
                        break;
                }
            }
            return InsertOnlyOne(request);
        }
    }
}
