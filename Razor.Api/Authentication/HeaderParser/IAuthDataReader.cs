using Microsoft.AspNetCore.Http;

namespace Razor.Api.Web.Authentication.HeaderParser
{
    public interface IAuthDataReader
    {
        AuthorizationData Read(HttpContext httpContext);
    }
}
