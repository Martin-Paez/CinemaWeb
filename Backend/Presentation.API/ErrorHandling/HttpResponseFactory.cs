using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.API.ErrorHandling
{
    public class HttpResponseFactory : IExceptionFilter
    {
        private ILogger<HttpResponseFactory> _logger;

        public HttpResponseFactory(ILogger<HttpResponseFactory> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var msg = context.Exception.Message;
            int code;
            if (context.Exception is InvalidArgumentsException)
                code = 400;
            else if (context.Exception is NoElementsException)
                code = 404;
            else if (context.Exception is ConflicException)
                code = 409;
            else
            {
                code = 500;
                msg = "Error inesperado en el servidor";
                _logger.LogError(
                    context.Exception,
                    "Error inesperado capturado en ResponseFactory"
                );
            }
            context.Result = new JsonResult( new { message = msg } ) 
                { 
                    StatusCode = code 
                };
            context.ExceptionHandled = true;
        }
    }
}