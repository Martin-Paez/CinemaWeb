using Application.Dto.Response.EntityProxy;
using Application.Interfaces.IServices.IMovie;
using ConsolePresenter.MenuCommandPattern.BasicPrinters;
using ConsoleView;

namespace Vista.Command_Pattern
{
    public class MoviesPrinter : ResponsePrinter<MovieResponse>
    {
        private IGetAllMoviesService _getMovies;

        public MoviesPrinter(
            IEnhacedConsole cmd, 
            IGetAllMoviesService getMovies
            )
            : base(cmd) 
        {
            _getMovies = getMovies;
        }

        protected override async Task<IList<MovieResponse>> GetTargets()
        {
            return await _getMovies.GetAll();
        }

        protected override string ToStr(MovieResponse movie)
        {
            return movie.Title;
        }

        protected override string MenuTitle()
        {
            return "Peliculas:\n----------\n";
        }
    }
}
