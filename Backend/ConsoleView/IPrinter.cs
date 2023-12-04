namespace ConsoleView
{
    public interface IPrinter
    {
        public ConsoleColor DefaultColor { get; set; }
        public void Print(string str);
        public void PrintYellow(string str);
        public void PrintRed(string str);
        public void PrintGreen(string str);
        public void PrintBlue(string str);
        public void PrintColor(string str, ConsoleColor color);
    }
}
