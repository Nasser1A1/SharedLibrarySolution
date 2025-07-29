using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.SharedLib.Middleware
{
    public class ListenToOnlyApiGateway(RequestDelegate next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the request is coming from the API Gateway
            var signedHeader = context.Request.Headers["Api-Gateway"];
            // NULL means the request is not from Api Gateway
            if (signedHeader.FirstOrDefault() is null)
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("This endpoint is only accessible via the API Gateway.");
                return;
            }
            else
            {
                await next(context);
            }
        }
    }
}
