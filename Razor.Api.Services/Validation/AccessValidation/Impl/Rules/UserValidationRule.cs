using FluentValidation;
using FluentValidation.Results;
using Razor.Api.DataAccess.Cache.Service;
using Razor.Api.DataAccess.Repository.Service.User;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.AccessValidation.Rules;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Impl.Rules
{
    public class UserValidationRule : AbstractValidator<CompanyTokenRequestModel>,  IUserValidationRule
    {
        private readonly IUserRepository _userRepository;
        private readonly ICompanyUserCacheService _companyUserCacheService;
        public UserValidationRule(IUserRepository userRepository, ICompanyUserCacheService companyUserCacheService)
        {
            _companyUserCacheService = companyUserCacheService;
            _userRepository = userRepository;
            RuleFor(t => t).Must(IsUserValid).WithMessage("Invalid User");



            //To DO: Implement Other validation rules
        }

        private bool IsUserValid(CompanyTokenRequestModel arg)
        {
            if (_companyUserCacheService.TryGetCompanyUserEnabledEntry(arg.CompanyId, arg.UserId, out var isEnabled)) return isEnabled;
            isEnabled = _userRepository.IsUserActive(arg.CompanyId, arg.UserId);
            _companyUserCacheService.SetCompanyUserEnabledEntry(arg.CompanyId, arg.UserId, isEnabled);
            return isEnabled;
        }
    }
}
