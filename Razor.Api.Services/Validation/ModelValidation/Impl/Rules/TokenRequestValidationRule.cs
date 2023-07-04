using FluentValidation;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.ModelValidation.Rules;

namespace Razor.Api.Services.V1.Validation.ModelValidation.Impl.Rules
{
    public class TokenRequestValidationRule : AbstractValidator<CompanyTokenRequestModel>, ITokenRequestValidationRule
    {
        public TokenRequestValidationRule()
        {
            RuleFor(x => x.CompanyId).NotEmpty();

            RuleFor(x => x.UserId).NotEmpty();

        }
    }
}
