using FluentValidation.Results;
using Razor.Api.Model.V1;
using System.Text;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Rules
{
    public interface ITokenValidationRule
    {
        ValidationResult Validate(CompanyUserModel model);
    }
}
