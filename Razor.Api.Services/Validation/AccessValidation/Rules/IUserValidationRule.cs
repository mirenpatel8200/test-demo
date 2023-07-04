using FluentValidation.Results;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Rules
{
    public interface IUserValidationRule
    {
        ValidationResult Validate(CompanyTokenRequestModel model);
    }
}
