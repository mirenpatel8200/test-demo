using FluentValidation;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.AccessValidation.Rules;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Impl
{
    public class CompanyAccessValidationService : ICompanyAccessValidationService
    {
        private readonly ICompanyValidationRule _companyValidationRule;
        public CompanyAccessValidationService(ICompanyValidationRule companyValidationRule)
        {
            _companyValidationRule = companyValidationRule;
        }

        public void Validate(CompanyModel model)
        {
            var validationResult = _companyValidationRule.Validate(model);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
