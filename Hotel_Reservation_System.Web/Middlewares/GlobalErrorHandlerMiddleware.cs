
using Hotel.Presentation.ViewModels.Response;

namespace Hotel_Reservation_System.Web.Middlewares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {    
                await next(context);
            }
            catch (Exception ex)
            {
               await HandleExceptionAsync(context, ex);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new FailedResponseViewModel(
                ErrorType.DatabaseConnectionError,
                "An unexpected error occurred"
            );

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
