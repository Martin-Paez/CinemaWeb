using System.Text;

namespace Application.Exceptions.Error
{
    public class ErrorTemplate
    {
        public int ErrorCode { get; }
        public string Message { get; set; }

        public ErrorTemplate(int errorCode, string message)
        {
            ErrorCode = errorCode;
            Message = message;
        }

        public string Build(params string[] parameters) 
        {
            var msg = string.Format(Message, parameters);
            return new StringBuilder()
                .Append("| Error ( ")
                .Append(ErrorCode)
                .Append(" ) : ")
                .Append(msg)
                .Append(" ")
                .ToString();
        }
    }
}
