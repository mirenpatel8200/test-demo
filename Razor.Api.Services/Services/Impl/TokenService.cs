using Razor.Api.Services.V1.CustomException;
using Razor.Api.DataAccess.Repository.Service.Token;
using Razor.Api.Model.V1;
using Razor.Api.DataAccess.Cache.Service;

namespace Razor.Api.Services.V1.Services.Impl
{
    public class TokenService : ITokenService
    {
        private readonly IUserTokenRepository _tokenRepository;
        private readonly ICompanyUserTokenCacheService _companyUserTokenCacheService;
        public TokenService(IUserTokenRepository tokenRepository, ICompanyUserTokenCacheService companyUserTokenCacheService)
        {
            _tokenRepository = tokenRepository;
            _companyUserTokenCacheService = companyUserTokenCacheService;
        }

        public void SaveToken(CompanyTokenRequestModel tokenRequest, string token)
        {
            _companyUserTokenCacheService.RemoveTokenEntry(tokenRequest.CompanyId, tokenRequest.UserId);
            _tokenRepository.SaveToken(tokenRequest.CompanyId, tokenRequest.UserId, token);
            _companyUserTokenCacheService.SetTokenEntry(tokenRequest.CompanyId, tokenRequest.UserId,token);
        }

        public string GetToken(CompanyTokenRequestModel tokenRequest)
        {
            if (_companyUserTokenCacheService.TryGetTokenEntry(tokenRequest.CompanyId, tokenRequest.UserId, out var token))
                return token; 
           
            token = _tokenRepository.GetToken(tokenRequest.CompanyId, tokenRequest.UserId);
            if (token == null)
            {
                throw new UserTokenNotFoundException("No maching record found for specified user id");
            }

            _companyUserTokenCacheService.SetTokenEntry(tokenRequest.CompanyId, tokenRequest.UserId, token);
            return token;
        }        
    }
}
