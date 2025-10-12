using System.Net;

namespace E_Commerce.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
               await _next(context);
               Console.WriteLine("Request completed successfully");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception occurred while processing the Request");


                var Result = new {
                    StatusCodes = (int)HttpStatusCode.InternalServerError,
                    Message = "There is an error in this request , Please Try again"
                };

                await context.Response.WriteAsJsonAsync(Result);
            }
        }
    }
}
