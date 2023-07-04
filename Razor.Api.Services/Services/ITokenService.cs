using Razor.Api.Model.V1;

namespace Razor.Api.Services.V1.Services
{
    public interface ITokenService
    {
        void SaveToken(CompanyTokenRequestModel tokenRequest, string token);
        string GetToken(CompanyTokenRequestModel tokenRequest);
    }
}
