using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Validation.AccessValidation
{
    public interface IUserAccessValidationService
    {
        void Validate(CompanyTokenRequestModel model);

        void ValidateUserToken(CompanyUserModel model);
    }
}
