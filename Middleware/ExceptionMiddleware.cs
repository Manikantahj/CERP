using System.Net;
using System.Text.Json;
namespace CERP.Middleware
{
   public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // next middleware/controller call
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new
            {
                is_success = false,
                msg = "Something went wrong",
                extra_info = ex.Message // debug purpose
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}
