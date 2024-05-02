using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Validation.AccessValidation
{
    public interface ICompanyAccessValidationService
    {
        void Validate(CompanyModel model);
    }
}
