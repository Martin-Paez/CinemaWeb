using Application.Dto.Response.EntityProxy;
using Application.Interfaces.IServices.IMovie;
using Application.Interfaces.IServices.IShow;
using ConsolePresenter.MenuCommandPattern.FilteringPrinters;
using ConsoleView;

namespace Vista.Command_Pattern
{
    public class ShowsByMovieOrDayPrinter : FilteringPrinter<ShowResponse>
    {
        private IGetAllMoviesService _getMovies;
        private IGetShowByService _getShows;

        public ShowsByMovieOrDayPrinter(
            IEnhacedConsole cmd,
            IGetAllMoviesService getMovies,
            IGetShowByService getShow
            )
            : base(cmd)
        {
            _getMovies = getMovies;
            _getShows = getShow;
        }

        /// <exception cref="CanceledEntry"></exception>
        protected override IList<ShowResponse> GetFiltereds()
        {
            var printer = new MoviesPrinter(_cmd, _getMovies);
            int id = printer.SelectOne().MovieId;
            return null!; //_getShows.GetShowsByMovieOrDay(id, _cmd.ReadDate());
        }
    }
}
