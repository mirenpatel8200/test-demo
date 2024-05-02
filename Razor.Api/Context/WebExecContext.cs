using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Razor.Api.Context;
using Razor.Api.Context.Models;

namespace Razor.Api.Web.Context
{
    public class WebExecContext : IExecContext
    {
        public ContextData Data { get; }

        public WebExecContext(IHttpContextAccessor httpContentAccessor, ILogger<WebExecContext> logger)
        {
            const string key = "WebExecContext:Data";
            var context = httpContentAccessor.HttpContext;
            if (context.Items[key] is ContextData storedInHttpContext)
            {
                Data = storedInHttpContext;
                return;
            }

            var user = context.User;
            if (user != null)
            {
                Data = new ContextData
                {
                    CompanyId = Convert.ToInt32(user.FindFirst("CompanyId")?.Value),
                    UserId    = Convert.ToInt32(user.FindFirst("UserId")?.Value)
                };
                context.Items[key] = Data;
                logger.LogDebug("WebExecContext.Data constructed for {CompanyId}-{UserId}", Data.CompanyId, Data.UserId);
            }
            else
            {
                logger.LogDebug("WebExecContext.Data failed to construct - no HttpContext user");
            }
        }

    }
}
