using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Razor.Api.Web.Authentication.TokenSource
{
    public interface ITokenSource
    {
        TokenModel GenerateTokens(Claim[] claims, DateTime now);
        (ClaimsPrincipal Claims, JwtSecurityToken JwtSecurityToken) GetTokenClaims(string token);
    }
}
