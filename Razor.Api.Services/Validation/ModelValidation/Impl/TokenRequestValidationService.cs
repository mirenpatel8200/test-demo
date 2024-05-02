using FluentValidation;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.ModelValidation.Rules;

namespace Razor.Api.Services.V1.Validation.ModelValidation.Impl
{
    public class TokenRequestValidationService : ITokenRequestValidationService
    {
        private readonly ITokenRequestValidationRule _tokenRequestValidationRule;
        public TokenRequestValidationService(ITokenRequestValidationRule tokenRequestValidationRule)
        {
            this._tokenRequestValidationRule = tokenRequestValidationRule;
        }
        public ITokenRequestValidationService Validate(CompanyTokenRequestModel model)
        {
            var validationResult = _tokenRequestValidationRule.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            return this;
        }
    }
}
