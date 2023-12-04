
namespace ConsoleView
{
    public interface IReader
    {
        /// <exception cref="CanceledEntry"></exception>
        public DateTime ReadDate();

        /// <exception cref="CanceledEntry"></exception>
        public TimeSpan ReadTime();

        /// <exception cref="CanceledEntry"></exception>
        public int ReadOption(int max);
    }
}
