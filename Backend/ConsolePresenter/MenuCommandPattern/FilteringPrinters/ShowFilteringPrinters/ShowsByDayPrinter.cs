using Application.Dto.Response.EntityProxy;
using Application.Interfaces.IServices.IShow;
using ConsolePresenter.MenuCommandPattern.FilteringPrinters;
using ConsoleView;

namespace Vista.Command_Pattern
{
    public class ShowsByDayPrinter : FilteringPrinter<ShowResponse>
    {
        private IGetShowByService _getShows;

        public ShowsByDayPrinter(
            IEnhacedConsole cmd, 
            IGetShowByService getShows
            )
            : base(cmd)
        {
            _getShows = getShows;
        }

        /// <exception cref="CanceledEntry"></exception>
        protected override IList<ShowResponse> GetFiltereds()
        {
            return null!;//_getShows.GetShowsByDay(_cmd.ReadDate());
        }
    }
}
