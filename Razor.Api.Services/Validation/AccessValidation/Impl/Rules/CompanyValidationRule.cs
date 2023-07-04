using FluentValidation;
using Razor.Api.DataAccess.Cache.Service;
using Razor.Api.DataAccess.Repository.Service.Company;
using Razor.Api.Model.V1;
using Razor.Api.Services.V1.Validation.AccessValidation.Rules;

namespace Razor.Api.Services.V1.Validation.AccessValidation.Impl.Rules
{
    public class CompanyValidationRule :AbstractValidator<CompanyModel>,ICompanyValidationRule
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly ICompanyAccessCacheService _companyAccessCacheService;
        public CompanyValidationRule(ICompanyRepository companyRepository, ICompanyAccessCacheService companyKeyCacheService)
        {
            _companyRepository = companyRepository;
            _companyAccessCacheService = companyKeyCacheService;
            RuleFor(x => x).Must(IsApiAccessEnabled).WithMessage("API access is disabled");
        }

        private bool IsApiAccessEnabled(CompanyModel arg)
        {
            if (_companyAccessCacheService.TryGetApiAccessEntry(out var isEnabled)) return isEnabled;
            isEnabled = _companyRepository.IsApiAccessEnabled(arg.CompanyId);
            _companyAccessCacheService.SetApiAccessEntry(isEnabled); 
            return isEnabled;
        }
    }
}
