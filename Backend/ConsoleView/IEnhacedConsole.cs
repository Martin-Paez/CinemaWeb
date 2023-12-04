
namespace ConsoleView
{
    public interface IEnhacedConsole : IPrinter, IReader
    {
        public void PrintCollection<T>(ICollection<T> col, Func<T, string> toStr,
            ConsoleColor numColor = ConsoleColor.Blue);
        public void PrintCollection<T>(ICollection<T> col) where T : notnull;
    }
}
