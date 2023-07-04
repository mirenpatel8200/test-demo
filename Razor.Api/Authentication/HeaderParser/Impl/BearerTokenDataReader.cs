using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Razor.Api.Web.Authentication.TokenSource;


namespace Razor.Api.Web.Authentication.HeaderParser.Impl
{
    public class BearerTokenDataReader : IAuthDataReader
    {
        private readonly ITokenSource _tokenSource;
        private readonly ILogger<BearerTokenDataReader> _logger;
        public BearerTokenDataReader(ITokenSource tokenSource, ILogger<BearerTokenDataReader> logger)
        {
            _tokenSource = tokenSource;
            _logger = logger;
        }

        public AuthorizationData Read(HttpContext httpContext)
        {
            const string key = "BearerTokenDataReader:AuthorizationData";
            if (httpContext.Items[key] is AuthorizationData storedInHttpContext)
            {
                return storedInHttpContext;
            }

            const string headerName = "Authorization";
            using(_logger.BeginScope(headerName))
            {
                _logger.LogDebug("Parsing the authorization header");
                var authHeader = httpContext.Request.Headers[headerName].FirstOrDefault();
                if (authHeader == null)
                {
                    _logger.LogInformation("Auth header is missing");
                    return null;
                }

                var parts = authHeader.Split(" ");
                if (parts.Length < 2)
                {
                    _logger.LogInformation("Auth header value is missing");
                    return null;
                }

                var token = parts[1];
                var decodedToken = _tokenSource.GetTokenClaims(token);
                if (decodedToken.Claims == null || !decodedToken.Claims.Claims.Any())
                {
                    _logger.LogInformation("Auth header contains no claims");
                    return null;
                }

                var claimCompanyId = decodedToken.Claims.FindFirst("CompanyId");
                if (claimCompanyId?.Value == null || !int.TryParse(claimCompanyId.Value, out var companyId))
                {
                    _logger.LogInformation("The company id claim is missing");
                    return null;
                }

                var claimUserId = decodedToken.Claims.FindFirst("UserId");
                if (claimUserId?.Value == null || !int.TryParse(claimUserId.Value, out var userId))
                {
                    _logger.LogInformation("The user id claim is missing");
                    return null;
                }

                _logger.LogDebug("The header parsed as \"{CompanyId}-{UserId}\"", companyId, userId);
                var data = new AuthorizationData
                {
                    Token     = token,
                    CompanyId = companyId,
                    UserId    = userId,
                };
                httpContext.Items[key] = data;
                return data;
            }
        }
    }
}