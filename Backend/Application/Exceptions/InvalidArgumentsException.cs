using System.Text;

namespace Application.Exceptions
{
    public class InvalidArgumentsException : Exception
    {
        public InvalidArgumentsException(string msg) 
            : base( new StringBuilder()
                .Append("La informacion recibida en la solicitud es invalida. ")
                .Append(msg)  
                .ToString()
            )
        {

        }
    }
}
