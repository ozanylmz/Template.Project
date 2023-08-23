using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using Template.Project.Application.Middlewares.Exceptions;

namespace Template.Project.Application.Middlewares
{
    public class GlobalExceptionHandlingMiddlware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddlware> _logger;
        public GlobalExceptionHandlingMiddlware(
           ILogger<GlobalExceptionHandlingMiddlware> logger)
        {
            _logger = logger;
        }
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
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ProblemDetails problem;

            var exceptionType = ex.GetType();

            if (exceptionType == typeof(BadRequestException))
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.BadRequest,
                    Type = exceptionType.ToString(),
                    Title = "BadRequest",
                    Detail = ex.Message
                };
            }
            else if (exceptionType == typeof(NotFoundException))
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Type = exceptionType.ToString(),
                    Title = "NotFound",
                    Detail = ex.Message
                };
            }
            else if (exceptionType == typeof(ConflictException))
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.Conflict,
                    Type = exceptionType.ToString(),
                    Title = "Conflict",
                    Detail = ex.Message
                };
            }
            else
            {
                problem = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Type = exceptionType.ToString(),
                    Title = "InternalServerError",
                    Detail = ex.Message
                };
            }

            string exceptionResult = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)problem.Status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
    public static class GlobalExceptionHandlingMiddlwareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandlingMiddlware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<GlobalExceptionHandlingMiddlware>();
        }
    }
}
