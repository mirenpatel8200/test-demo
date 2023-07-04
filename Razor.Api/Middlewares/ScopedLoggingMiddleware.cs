using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System.Text;
using Razor.Api.Model.V1;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Controllers;
using Razor.Api.Web.Authentication.HeaderParser;

namespace Razor.Api.Web.Middlewares
{
    public class ScopedLoggingMiddleware
    {
        const string CompanyId = "CompanyID";
        const string UserId = "UserID";

        private readonly RequestDelegate _next;
        private readonly ILogger<ScopedLoggingMiddleware> _logger;
        private readonly IAuthDataReader _authDataReader;

        public ScopedLoggingMiddleware(RequestDelegate next, ILogger<ScopedLoggingMiddleware> logger, IAuthDataReader authDataReader)
        {
            _next             = next ?? throw new ArgumentNullException(nameof(next));
            _logger           = logger ?? throw new ArgumentNullException(nameof(logger));
            _authDataReader = authDataReader;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            try
            {
                var controllerActionDescriptor = httpContext
                        .GetEndpoint()
                        .Metadata
                        .GetMetadata<ControllerActionDescriptor>();
                var controllerName = controllerActionDescriptor.ControllerName;
                if (controllerName.ToLower() == "token" )
                {
                    // Extract the scope for non authenicated controller
                    httpContext.Request.EnableBuffering();

                    // Leave the body open so the next middleware can read it.
                    using (var reader = new StreamReader(
                        httpContext.Request.Body,
                        encoding: Encoding.UTF8,
                        detectEncodingFromByteOrderMarks: false,
                        bufferSize: 100,
                        leaveOpen: true))
                    {
                        var body = await reader.ReadToEndAsync();
                        // Do some processing with body…
                        var model = JsonConvert.DeserializeObject<CompanyTokenRequestModel>(body);

                        httpContext.Request.Body.Position = 0;
                        // Reset the request body stream position so the next middleware can read it

                        using (LogContext.PushProperty(CompanyId, model.CompanyId, true))
                        using (LogContext.PushProperty(UserId, model.UserId, true))
                        {
                            await _next.Invoke(httpContext);
                        }
                    }
                }
                else
                {
                    var authData = _authDataReader.Read(httpContext);
                    if (authData != null)
                    {
                        //Add as many nested usings as needed, for adding more properties 
                        using (LogContext.PushProperty(CompanyId, authData.CompanyId, true))
                        using (LogContext.PushProperty(UserId, authData.UserId, true))
                        {
                            await _next.Invoke(httpContext);
                        }
                    }
                }    
            }
            //To make sure that we don't loose the scope in case of an unexpected error
            catch (Exception ex) when (LogOnUnexpectedError(ex))
            {
            }
        }

        private bool LogOnUnexpectedError(Exception ex)
        {
            _logger.LogError(ex, "An unexpected exception occured!");
            return true;
        }
    }
}
