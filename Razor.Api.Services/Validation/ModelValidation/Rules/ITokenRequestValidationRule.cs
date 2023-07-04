using FluentValidation.Results;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Validation.ModelValidation.Rules
{
    public interface ITokenRequestValidationRule 
    {
        ValidationResult Validate(CompanyTokenRequestModel model);
    }
}
