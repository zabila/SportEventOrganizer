using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Sportalytics.Event.Domain.Exceptions;
using Sportalytics.Feed.Domain.Entities;
using NotFoundException = Sportalytics.Feed.Domain.Exceptions.NotFoundException;

namespace Sportalytics.Event.API.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError => {
            appError.Run(async context => {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    context.Response.StatusCode = contextFeature.Error switch
                    {
                        NotFoundException => StatusCodes.Status404NotFound,
                        ForbiddenException => StatusCodes.Status403Forbidden,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    Console.WriteLine($"Something went wrong: {contextFeature.Error}");
                    var errorDetails = new ErrorDetails
                    {
                        StatusCode = context.Response.StatusCode,
                        Message = contextFeature.Error.Message
                    };

                    await context.Response.WriteAsync(errorDetails.ToString());
                }
            });
        });
    }
}