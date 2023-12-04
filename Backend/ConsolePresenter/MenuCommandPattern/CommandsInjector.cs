using Application.Interfaces.IServices.IMovie;
using Application.Interfaces.IServices.IRoom;
using Application.Interfaces.IServices.IShow;
using ConsolePresenter.MenuCommandPattern.BasicPrinters.ConcreteBasicPrinters;
using ConsolePresenter.MenuCommandPattern.FilteringPrinters.ShowFilteringPrinters;
using ConsolePresenter.MenuCommandPattern.Inserters;
using Vista.Command_Pattern;

namespace ConsolePresenter.MenuCommandPattern
{
    public class CommandsInjector
    {
        public IGetShowByService GetShows { get; }
        public IAddShowService AddShow { get; }
        public IGetAllRoomsService GetRooms { get; }
        public IGetAllMoviesService GetMovies { get; }
        public MoviesPrinter MoviesPrinter { get; }
        public RoomsPrinter RoomsPrinter { get; }
        public ShowsByDayPrinter ShowsByDayPrinter { get; }
        public ShowsByMoviePrinter ShowsByMoviePrinter { get; }
        public ShowsByMovieAndDayPrinter ShowsByMovieAndDayPrinter { get; }
        public ShowsByMovieOrDayPrinter ShowsByMovieOrDayPrinter { get; }
        public ShowsInserter ShowsInserter { get; }

        public CommandsInjector(
            MoviesPrinter moviesPrinter,
            RoomsPrinter roomsPrinter,
            ShowsByDayPrinter showsByDayPrinter,
            ShowsByMoviePrinter showsByMoviePrinter,
            ShowsByMovieAndDayPrinter showsByMovieAndDayPrinter,
            ShowsByMovieOrDayPrinter showsByMovieOrDayPrinter,
            IGetShowByService getShows,
            IAddShowService addShow,
            IGetAllRoomsService getRooms,
            IGetAllMoviesService getMovies,
            ShowsInserter showsInserter)
        {
            MoviesPrinter = moviesPrinter;
            RoomsPrinter = roomsPrinter;
            ShowsByDayPrinter = showsByDayPrinter;
            ShowsByMoviePrinter = showsByMoviePrinter;
            ShowsByMovieAndDayPrinter = showsByMovieAndDayPrinter;
            ShowsByMovieOrDayPrinter = showsByMovieOrDayPrinter;
            GetShows = getShows;
            AddShow = addShow;
            GetRooms = getRooms;
            GetMovies = getMovies;
            ShowsInserter = showsInserter;
        }
    }
}
