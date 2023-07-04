using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.DataAccess.Cache.Service;

namespace Razor.Api.Web.Cache.Service
{
    public class CompanyUserCacheService : ICompanyUserCacheService
    {

        private readonly ICacheManager _cacheManager;
        public CompanyUserCacheService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        private string BuildKey(long companyId, long userId)
           => $"{companyId}-{userId}-is-valid-user";

        public void SetCompanyUserEnabledEntry(long companyId, long userId, bool isEnabled)
        {
            _cacheManager.SetValue<bool>(BuildKey(companyId, userId), isEnabled);   
        }

        public bool TryGetCompanyUserEnabledEntry(long companyId, long userId, out bool isEnabled)
        {
            return _cacheManager.TryGetValue<bool>(BuildKey(companyId, userId), out isEnabled);
        }
    }
}
