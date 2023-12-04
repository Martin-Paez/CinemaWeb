
namespace ConsoleView
{
    public class Printer : IPrinter
    {
        public ConsoleColor DefaultColor
        {
            get { return Console.ForegroundColor; }
            set { Console.ForegroundColor = value; }
        }

        public Printer()
        {
            DefaultColor = ConsoleColor.White;
        }

        public void PrintYellow(string str)
        {
            PrintColor(str, ConsoleColor.Yellow);
        }
        public void PrintBlue(string str)
        {
            PrintColor(str, ConsoleColor.Blue);
        }

        public void PrintRed(string str)
        {
            PrintColor(str, ConsoleColor.Red);
        }

        public void PrintGreen(string str)
        {
            PrintColor(str, ConsoleColor.Green);
        }

        public void PrintColor(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Print(string str = "")
        {
            Console.Write(str);
        }
    }
}
