using ConsoleView;

namespace ConsolePresenter.MenuCommandPattern
{
    public class MenuInvoker : ICommand
    {
        private Dictionary<int, ICommand> _commands;
        private IEnhacedConsole _cmd;
        private string _menuText;
        private string _title;

        public MenuInvoker(
            IEnhacedConsole cmd, 
            string title = "Menu:\n-----\n\n"
            )
        {
            _commands = new Dictionary<int, ICommand>();
            _cmd = cmd;
            _menuText = "";
            _title = title;
        }

        public void Add(ICommand command, string menuIem)
        {
            _commands.Add(_commands.Count, command);
            addTextItem(menuIem);
        }

        /// <exception cref="CanceledEntry"></exception>
        public bool Select()
        {
            _cmd.PrintYellow("\n" + _title);
            _cmd.Print(_menuText + "\n");
            int option;
            try{
                option = _cmd.ReadOption(_commands.Count) - 1;
            }catch (CanceledEntry) {
                _cmd.PrintBlue("\nComo no desea elegir una opcion se cierra el menu.\n\n");
                return false;
            }
            if (_commands.ContainsKey(option))
                try {
                    return _commands[option].Execute();
                } catch (CanceledEntry) {
                    _cmd.PrintBlue("\nYa que no desea completar la accion se regresa al menu.\n\n");
                }
            return true;
        }

        public void Run()
        {
            while (Select()) ;
        }

        private void addTextItem(string menuItem)
        {
            _menuText += "   " + _commands.Count.ToString() + ") " + menuItem + "\n";
        }

        public void AddExit(string label = "Salir")
        {
            _commands.Add(_commands.Count, new Exit());
            addTextItem(label);
        }

        public bool Execute()
        {
            Run();
            return true;
        }

        private class Exit : ICommand
        {
            public bool Execute()
            {
                return false;
            }
        }
    }
}