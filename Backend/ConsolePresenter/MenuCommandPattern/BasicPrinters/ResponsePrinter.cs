using ConsoleView;

namespace ConsolePresenter.MenuCommandPattern.BasicPrinters
{
    public abstract class ResponsePrinter<Target> : ICommand
    {
        protected IEnhacedConsole _cmd;

        public ResponsePrinter(IEnhacedConsole cmd)
        {
            _cmd = cmd;
        }

        public bool Execute()
        {
            PrintTargets();
            return true;
        }

        public IList<Target> PrintTargets()
        {
            _cmd.PrintGreen(MenuTitle());
            IList<Target> targets = GetTargets();
            _cmd.PrintCollection(targets, ToStr);
            _cmd.Print("\n");
            return targets;
        }

        protected virtual string ToStr(Target target)
        {
            if (target == null)
                return "";
            var str = target.ToString();
            return str == null ? "" : str;
        }

        protected virtual string MenuTitle()
        {
            return "Menu:\n-----\n";
        }

        /// <exception cref="CanceledEntry"></exception>
        public Target SelectOne()
        {
            IList<Target> targets = PrintTargets();
            int n = _cmd.ReadOption(targets.Count);
            return targets[n - 1];
        }

        protected abstract IList<Target> GetTargets();
    }
}