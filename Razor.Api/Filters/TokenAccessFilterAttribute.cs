using Microsoft.AspNetCore.Mvc.Filters;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.AccessValidation;


namespace Razor.Api.Web.Filters
{
    public class TokenAccessFilterAttribute : IActionFilter
    {
        private readonly IUserAccessValidationService _userAccessValidationService;
        public TokenAccessFilterAttribute(IUserAccessValidationService userAccessValidationService)
        {
            _userAccessValidationService = userAccessValidationService;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
           
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var model = context.ActionArguments["tokenRequest"] as CompanyTokenRequestModel;
           _userAccessValidationService.Validate(model);
        }
    }
}
