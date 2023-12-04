using ConsoleView;

namespace ConsolePresenter.MenuCommandPattern.FilteringPrinters
{
    public abstract class FilteringPrinter<Target> 
        : ICommand
        where Target : class
    {
        protected IEnhacedConsole _cmd;

        public FilteringPrinter(IEnhacedConsole cmd)
        {
            _cmd = cmd;
        }

        /// <exception cref="CanceledEntry"></exception>
        public bool Execute()
        {
            _cmd.PrintCollection(GetFiltereds());
            _cmd.Print("\n");
            return true;
        }

        protected abstract IList<Target> GetFiltereds();
    }
}