using Microsoft.AspNetCore.Mvc.Filters;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.AccessValidation;
using System;
using Razor.Api.Web.Authentication.HeaderParser;

namespace Razor.Api.Web.Filters
{
    public class AccessFilterAttribute : IActionFilter
    {
        private readonly ICompanyAccessValidationService _companyAccessValidationService;
        private readonly IUserAccessValidationService _userAccessValidationService;
        private readonly IAuthDataReader _authDataReader;

        public AccessFilterAttribute(IAuthDataReader authDataReader, IUserAccessValidationService userAccessValidationService, ICompanyAccessValidationService companyAccessValidationService)
        {
            _companyAccessValidationService = companyAccessValidationService;
            _userAccessValidationService    = userAccessValidationService;
            _authDataReader               = authDataReader;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var authData = _authDataReader.Read(context.HttpContext);
            if (authData == null)
                throw new UnauthorizedAccessException("The authorization header is either missing or invalid");
            
            // throw the exception if API access is disabled
            _companyAccessValidationService.Validate(new CompanyModel { CompanyId = authData.CompanyId });

            // Throws exception if token is Invalid
            _userAccessValidationService.ValidateUserToken(new CompanyUserModel
            {
                CompanyId = authData.CompanyId,
                UserId = authData.UserId,
                Token = authData.Token
            });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
