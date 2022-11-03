using System.Net;
using System.Text.Json;

namespace ShareVideosAPI.Middlewares.Errors
{
    /// <summary>
    /// Middleware to handle error messages
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Instantiate ErrorHandlerMiddleware
        /// </summary>
        /// <param name="next"></param>
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Handle error messages to remove details and return only the message.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case TaskCanceledException e:
                        response.StatusCode = (int)HttpStatusCode.GatewayTimeout;
                        break;
                    case KeyNotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case BadHttpRequestException e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
