using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Razor.Api.Web.Config;

namespace Razor.Api.Web.Authentication.TokenSource.Impl
{
    public class TokenSource : ITokenSource
    {
        private readonly ILogger<TokenSource> _logger;
        private readonly TokenConfig _jwtTokenConfig;
        private readonly byte[] _secret;

        public TokenSource(TokenConfig jwtTokenConfig, ILogger<TokenSource> logger)
        {
            _jwtTokenConfig = jwtTokenConfig;
            _logger         = logger;
            _secret         = Encoding.UTF8.GetBytes(jwtTokenConfig.Secret);
        }

        public (ClaimsPrincipal Claims, JwtSecurityToken JwtSecurityToken) GetTokenClaims(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("The token is empty");
            }

            var principal = new JwtSecurityTokenHandler()
                .ValidateToken(token,
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _jwtTokenConfig.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(_secret),
                        ValidAudience = _jwtTokenConfig.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = false,
                        ClockSkew = TimeSpan.FromMinutes(1)
                    },
                    out var validatedToken);
            return (principal, validatedToken as JwtSecurityToken);
        }

        public TokenModel GenerateTokens(Claim[] claims, DateTime now)
        {
            _logger.LogDebug("Generating the tokens");
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            
            var securityToken = new JwtSecurityToken(
                _jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256));
            
            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);
            
            _logger.LogDebug("The access token is: {AccessToken}", accessToken);
            return new TokenModel
            {
                AccessToken = accessToken
            };
        }
    }
}
