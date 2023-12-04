using System.Globalization;

namespace ConsoleView
{
    public class Reader : IReader
    {
        private IPrinter _printer;

        public Reader(IPrinter console)
        {
            _printer = console;
        }

        public delegate bool Parser<T>(string value, out T result);

        /// <exception cref="CanceledEntry"></exception>
        private T Read<T>(string query, string errMsg, Parser<T> parser)
        {
            T result;
            bool notOk;
            do
            {
                _printer.PrintGreen(query);
                string opt = ReadStringForced();
                notOk = !parser(opt!, out result);
                if (notOk && !AskForRetry(errMsg))
                    throw new CanceledEntry();
                Console.WriteLine();
            } while (notOk);
            return result;
        }

        private bool AskForRetry(string errMsg)
        {
            string opt;
            _printer.PrintRed(
                        errMsg + "\n" +
                        "Quiere intentar de nuevo ? (S/N) : ");
            do
            {
                opt = ReadStringForced().ToUpper();
            } while (opt != "N" && opt != "S");
            _printer.Print("\n");
            return opt == "S";
        }

        private string ReadStringForced()
        {
            string? opt;
            while ((opt = Console.ReadLine()) == null) ;
            return opt;
        }

        /// <exception cref="CanceledEntry"></exception>
        public DateTime ReadDate()
        {
            string msg = "Ingrese una fecha (yyyy/mm/dd): ";
            return Read<DateTime>(
                msg,
                "\nEl valor ingresado no es una fecha valida. Por ejemplo: 2023/12/25\n",
                ParseDateTime);
        }

        private bool ParseDateTime(string value, out DateTime result)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            string format = "yyyy/MM/dd";
            DateTimeStyles style = DateTimeStyles.None;
            return DateTime.TryParseExact(value, format, culture, style, out result);
        }

        /// <exception cref="CanceledEntry"></exception>
        public TimeSpan ReadTime()
        {
            string msg = "Ingrese un horario (hh:mm): ";
            return Read<TimeSpan>(
                msg,
                "\nEl valor ingresado no es un horario valido. Por ejemplo: 22:45\n", 
                ParseTimeSpan);
        }

        private bool ParseTimeSpan(string value, out TimeSpan result)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            string format = "h\\:mm";
            TimeSpanStyles style = TimeSpanStyles.None;
            return TimeSpan.TryParseExact(value, format, culture, style, out result);
        }

        /// <exception cref="CanceledEntry"></exception>
        public int ReadOption(int max)
        {
            int n = -1;
            bool loop = true;
            while (loop)
            {
                n = Read<int>(
                    "Ingrese una numero de opcion: ",
                    "\nEl valor ingresado debe ser un numero entero (sin coma, ni puntos).\n",
                    int.TryParse);
                loop = n < 1 || n > max;
                if (loop)
                    if(!AskForRetry("\nEl valor ingresado debe ser mayor a 0 y menor a " + max + ".\n"))
                        throw new CanceledEntry();
            }
            return n;
        }
    }

}
