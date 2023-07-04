using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Services;
using Razor.Api.Services.V1.Validation.ModelValidation;
using Razor.Api.Web.Attribute;
using Razor.Api.Web.Authentication.TokenSource;
using Razor.Api.Web.Filters;

namespace Razor.Api.Web.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ServiceFilter(typeof(TokenAccessFilterAttribute))]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly ITokenSource _tokenSource;
        private readonly ITokenRequestValidationService _tokenValidationService;
        public TokenController(ITokenService tokenService, ITokenSource tokenSource, ITokenRequestValidationService tokenValidationService)
        {
            _tokenService = tokenService;
            _tokenSource = tokenSource;
            _tokenValidationService = tokenValidationService;
        }


        [AllowAnonymous]
        [HttpPost("GenerateToken")]
        [MapToApiVersion("1")]
        [ValidateModelState]
        public ActionResult GenerateToken([FromBody] CompanyTokenRequestModel tokenRequest)
        {
            // validate Model
            _tokenValidationService.Validate(tokenRequest);
            var claims = new[]
            {
                new Claim("CompanyId",tokenRequest.CompanyId.ToString()),
                new Claim("UserId", tokenRequest.UserId.ToString())
            };

            var tokenResult = _tokenSource.GenerateTokens(claims, DateTime.Now);
            _tokenService.SaveToken(tokenRequest, tokenResult.AccessToken);
            return Ok(new { Token = tokenResult.AccessToken });
        }

        [AllowAnonymous]
        [HttpPost("GetToken")]
        [MapToApiVersion("1")]
        [ValidateModelState]
        public ActionResult GetToken([FromBody] CompanyTokenRequestModel tokenRequest)
        {
            // validate Model
            _tokenValidationService.Validate(tokenRequest);
            var token = _tokenService.GetToken(tokenRequest);
            return Ok(new { Token = token });
        }
    }
}
