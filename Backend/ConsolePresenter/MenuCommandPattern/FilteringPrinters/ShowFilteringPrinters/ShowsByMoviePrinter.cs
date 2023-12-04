using Application.Dto.Response.EntityProxy;
using Application.Interfaces.IServices.IMovie;
using Application.Interfaces.IServices.IShow;
using ConsoleView;
using Vista.Command_Pattern;

namespace ConsolePresenter.MenuCommandPattern.FilteringPrinters.ShowFilteringPrinters
{
    public class ShowsByMoviePrinter : FilteringPrinter<ShowResponse>
    {
        private IGetShowByService _getShows;
        private IGetAllMoviesService _getAllMovies;

        public ShowsByMoviePrinter(
            IEnhacedConsole cmd,
            IGetShowByService getShows,
            IGetAllMoviesService getAllMovies)
            : base(cmd)
        {
            _getShows = getShows;
            _getAllMovies = getAllMovies;
        }

        /// <exception cref="CanceledEntry"></exception>
        protected override IList<ShowResponse> GetFiltereds()
        {
            var printer = new MoviesPrinter(_cmd, _getAllMovies);
            var id = printer.SelectOne().MovieId;
            return null!;// _getShows.GetShowsByMovie(id);
        }
    }
}