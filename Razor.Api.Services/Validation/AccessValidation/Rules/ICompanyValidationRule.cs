using FluentValidation.Results;
using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Rules
{
    public interface ICompanyValidationRule
    {
        ValidationResult Validate(CompanyModel model);
    }
}
