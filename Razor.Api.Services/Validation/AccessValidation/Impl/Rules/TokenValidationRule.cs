using FluentValidation;
using Razor.Api.DataAccess.Cache.Service;
using Razor.Api.DataAccess.Repository.Service.Token;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.AccessValidation.Rules;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Impl.Rules
{
    public class TokenValidationRule : AbstractValidator<CompanyUserModel>, ITokenValidationRule
    {
        private readonly IUserTokenRepository _tokenRepository;
        private readonly ICompanyUserTokenCacheService _companyUserTokenCacheService;
        public TokenValidationRule(IUserTokenRepository tokenRepository, ICompanyUserTokenCacheService companyUserTokenCacheService)
        {
            _tokenRepository = tokenRepository;
            _companyUserTokenCacheService = companyUserTokenCacheService;
            RuleFor(t => t).Must(IsTokenValid).WithMessage("Invalid token");
            //To DO: Implement Other validation rules
        }

        private bool IsTokenValid(CompanyUserModel arg)
        {
            if (!_companyUserTokenCacheService.TryGetTokenEntry(arg.CompanyId, arg.UserId, out var token))
                token = _tokenRepository.GetToken(arg.CompanyId, arg.UserId);

            if (token != arg.Token)
                return false;

            _companyUserTokenCacheService.SetTokenEntry(arg.CompanyId, arg.UserId, token); // refresh the cache
            return true;
        }
    }
}
