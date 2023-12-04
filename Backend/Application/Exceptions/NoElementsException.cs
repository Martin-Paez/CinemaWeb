namespace Application.Exceptions
{
    public class NoElementsException : Exception
    {
        public NoElementsException(string msg) 
            : base("Recurso no encontrado. " + msg) 
        {
        }
    }
}
