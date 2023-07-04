using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.DataAccess.Cache.Service;

namespace Razor.Api.Web.Cache.Service
{
    public class CompanyAccessCacheService : ICompanyAccessCacheService
    {
        private readonly ICompanyScopeCacheManager _companyScopeCacheManager;
        public CompanyAccessCacheService(ICompanyScopeCacheManager companyScopeCacheManager)
        {
            _companyScopeCacheManager = companyScopeCacheManager;
        }

        private string BuildKey()
           => $"{_companyScopeCacheManager.GetCompanyPrefix()}-api-access-enabled";
        public bool TryGetApiAccessEntry(out bool isEnabled)
        {
           return _companyScopeCacheManager.TryGetValue<bool>(BuildKey(), out isEnabled);
        }

        public void SetApiAccessEntry(bool isEnabled)
        {
            _companyScopeCacheManager.SetValue<bool>(BuildKey(), isEnabled);
        }
    }
}
