using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Razor.Api.Context;
using Razor.Api.Context.Models;
using System;

namespace Razor.Api.WebContext
{
    public class WebExecContext : IExecContext
    {
        public WebExecContext(IHttpContextAccessor httpContentAccessor, IConfiguration configuration)
        {
            Data = new ContextData
            {
                CompanyId = Convert.ToInt32(httpContentAccessor.HttpContext.User.FindFirst("CompanyId")?.Value),
                UserId = Convert.ToInt32(httpContentAccessor.HttpContext.User.FindFirst("UserId")?.Value)
            };

            Configuration = configuration;
        }
        public ContextData Data { get; set; }
        public IConfiguration Configuration { get; }


    }
}
