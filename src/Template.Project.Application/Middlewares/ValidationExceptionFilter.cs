using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentValidation;

namespace Template.Project.Application.Middlewares
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var problemDetail = new ProblemDetails
                {
                    Status = (int)HttpStatusCode.NotFound,
                    Title = "One or more validation errors occurred.",
                    Detail = "See the 'errors' property for details.",
                };

                foreach (var error in validationException.Errors)
                {
                    problemDetail.Extensions.Add(error.PropertyName, new[] { error.ErrorMessage });
                }

                context.Result = new BadRequestObjectResult(problemDetail);
                context.ExceptionHandled = true;
            }
        }
    }
}
