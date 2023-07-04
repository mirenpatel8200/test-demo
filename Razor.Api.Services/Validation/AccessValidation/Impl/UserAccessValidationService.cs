using FluentValidation;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.AccessValidation.Rules;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Impl
{
    public class UserAccessValidationService : IUserAccessValidationService
    {
        private readonly IUserValidationRule _userValidationRule;
        private readonly ITokenValidationRule _tokenValidationRule;

        public UserAccessValidationService(IUserValidationRule userValidationRule, ITokenValidationRule tokenValidationRule)
        {
            _userValidationRule = userValidationRule;
            _tokenValidationRule = tokenValidationRule;
        }

        public void Validate(CompanyTokenRequestModel model)
        {
            var validationResult = _userValidationRule.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        public void ValidateUserToken(CompanyUserModel model)
        {
            var validationResult = _tokenValidationRule.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
