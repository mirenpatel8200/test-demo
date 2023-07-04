using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Razor.Api.Services.V1.CustomException;
using System;
using System.Net;
using Microsoft.IdentityModel.Tokens;
using Razor.Api.DataAccess.CustomException;
using Razor.Api.Web.Filters.Model;
using Microsoft.Extensions.Logging;

namespace Razor.Api.Web.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        private const string JsonContentType = "application/json";
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var handled = Handle(context.Exception);
            if (handled != null)
            {
                context.ExceptionHandled = true;
                var response = context.HttpContext.Response;
                response.StatusCode  = (int)handled.Value.Code;

                response.ContentType = JsonContentType;
                context.Result       = new ObjectResult(handled.Value.Details);
                _logger.LogError(context.Exception, $"A handled exception: {context.Exception.Message}. Returning status code HTTP{response.StatusCode}");
            }
            else
            {
                _logger.LogError(context.Exception, $"An unhandled exception: {context.Exception.Message}");
            }
        }

        private static (HttpStatusCode Code, ApiExceptionDetails Details)? Handle(Exception exception)
        {
            switch (exception)
            {
                case ValidationException _:
                    return (HttpStatusCode.BadRequest, new ApiExceptionDetails("The model is invalid", exception));
                case UserTokenNotFoundException _:
                    return (HttpStatusCode.BadRequest, new ApiExceptionDetails("The user is not allowed to access the API", exception));
                case CompanyNotFoundException _:
                    return (HttpStatusCode.BadRequest, new ApiExceptionDetails("The company is not allowed to access the API", exception));
                case SecurityTokenException _:
                    return (HttpStatusCode.BadRequest, new ApiExceptionDetails("The provided access token is invalid", exception));
                default:
                    return (HttpStatusCode.InternalServerError, new ApiExceptionDetails("An unexpected exception", exception));
            }
        }
    }
}
