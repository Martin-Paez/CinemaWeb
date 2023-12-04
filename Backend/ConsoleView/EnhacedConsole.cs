namespace ConsoleView
{
    public class EnhacedConsole : IEnhacedConsole
    {
        private IPrinter _printer;
        private IReader _reader;
        public ConsoleColor DefaultColor
        {
            get { return _printer.DefaultColor; }
            set { _printer.DefaultColor = value; }
        }

        public EnhacedConsole(IPrinter printer, IReader reader)
        {
            _printer = printer;
            _reader = reader;
        }

        public void PrintCollection<T>(ICollection<T> col, Func<T, string> toStr,
            ConsoleColor numColor = ConsoleColor.Blue)
        {
            if (col.Count == 0)
            {
                Print("Sin elementos\n");
                return;
            }
            int i = 0;
            Print();
            foreach (var response in col)
            {
                PrintColor(++i + ") ", numColor);
                Print(toStr(response) + "\n");
            }
            Print();
        }

        public void PrintCollection<T>(ICollection<T> col) where T : notnull
        {
            Func<T, string> toStr = (r) => {
                var txt = r.ToString();
                return (txt == null) ? "" : txt; 
            };
            PrintCollection(col, toStr);
        }

        public void Print(string str = "")
        {
            _printer.Print(str);
        }

        public void PrintColor(string str, ConsoleColor color)
        {
            _printer.PrintColor(str, color);
        }

        public void PrintGreen(string str)
        {
            _printer.PrintGreen(str);
        }
        public void PrintBlue(string str)
        {
            _printer.PrintBlue(str);
        }

        public void PrintYellow(string str)
        {
            _printer.PrintYellow(str);
        }

        public void PrintRed(string str)
        {
            _printer.PrintRed(str);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CanceledEntry"></exception>
        public TimeSpan ReadTime()
        {
            return _reader.ReadTime();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CanceledEntry"></exception>
        public DateTime ReadDate()
        {
            return _reader.ReadDate();
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CanceledEntry"></exception>
        public int ReadOption(int max)
        {
            return _reader.ReadOption(max);
        }
    }
}
