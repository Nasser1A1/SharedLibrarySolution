using eCommerce.SharedLib.Logs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace eCommerce.SharedLib.Middleware
{
    public class GlobalException(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Declare vars
            string message = "Internal Server Error Occurred, Please Try again later";
            int statusCode = (int)HttpStatusCode.InternalServerError; // 500
            string title = "Internal Server Error";

            try
            {
                await next(context);
                // Check if Exception is too many requests
                if (context.Response.StatusCode == StatusCodes.Status429TooManyRequests) { 
                title = "Too Many Requests";
                message = "You have exceeded the number of requests allowed. Please try again later.";
                statusCode = (int)StatusCodes.Status429TooManyRequests; // 429
                await ModifyHeader(context, title, message, statusCode);
                }
                // If Response is UnAuthorized
                if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
                {
                    title = "Authentication Error";
                    message = "You are not Authenticated.";
                    statusCode = (int)StatusCodes.Status401Unauthorized; // 401
                    await ModifyHeader(context, title, message, statusCode);
                }

                // If Response is forbidden
                if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
                {
                    title = "Authorization Error";
                    message = "You are not authorized To Access this Entity";
                    statusCode = (int)StatusCodes.Status403Forbidden; // 403
                    await ModifyHeader(context, title, message, statusCode);

                }
            }
            catch (Exception ex)
            {

                // Log Original Exception
                LogExceptions.LogException(ex);

                // Check if exception is time out
                if (ex is TaskCanceledException || ex is TimeoutException)
                {
                    title = "Request Time Out";
                    message = "Request Time Out, Please Try again";
                    statusCode = (int)StatusCodes.Status408RequestTimeout; // 408
          
                }
                // If Exception is caught
                // if none of the exceptions then do defualt
                await ModifyHeader(context, title, message, statusCode);

            }
        }

        private static async Task ModifyHeader(HttpContext context, string title, string message, int statusCode)
        {
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(
                new ProblemDetails
                {
                    Title = title,
                    Detail = message,
                    Status = statusCode,
                    Type = "https://httpstatuses.com/" + statusCode
                }
                ),CancellationToken.None);
            return;

        }
    }
}
    
    
