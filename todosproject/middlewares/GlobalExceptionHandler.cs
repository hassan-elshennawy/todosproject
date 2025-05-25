using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using todosproject.Entities;

namespace todosproject.middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                int statusCode;
                var errorResponse = new ErrorResponse();
                switch (ex)
                {
                    case ArgumentException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.message = "Wrong username or password";
                        break;
                    case UnauthorizedAccessException:
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        errorResponse.message = "you are unauthorized";
                        break;
                    case InvalidOperationException:
                        statusCode = (int)HttpStatusCode.Unauthorized;
                        errorResponse.message = "you can't do this operation";
                        break;
                    default:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.message = "error occured please try again later";
                        break;
                }
                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
