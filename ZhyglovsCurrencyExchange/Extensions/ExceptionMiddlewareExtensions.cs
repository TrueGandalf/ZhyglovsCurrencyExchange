using Microsoft.AspNetCore.Diagnostics;
using Refit;

namespace ZhyglovsCurrencyExchange.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(appBuilder =>
        {
            appBuilder.Run(async context =>
            {
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (exceptionHandlerFeature != null)
                {
                    var exception = exceptionHandlerFeature.Error;

                    // Log the exception here
                    // logger.LogError(exception, "An unhandled exception has occurred.");

                    // Prepare problem details
                    var problemDetails = new ProblemDetails
                    {
                        Title = "An error occurred while processing your request.",
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = exception.Message,
                        Instance = context.Request.Path
                    };

                    if (env.IsDevelopment())
                    {
                        problemDetails.Extensions["trace"] = exception.StackTrace;
                    }

                    // Write the response
                    context.Response.StatusCode = problemDetails.Status;//.Value
                    context.Response.ContentType = "application/problem+json";

                    await context.Response.WriteAsJsonAsync(problemDetails);
                }
            });
        });
    }
}
