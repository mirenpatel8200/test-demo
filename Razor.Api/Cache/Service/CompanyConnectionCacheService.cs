using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.DataAccess.Cache.Service;

namespace Razor.Api.Web.Cache.Service
{
    public class CompanyConnectionCacheService : ICompanyConnectionCacheService
    {
      
        private readonly ICacheManager _cacheManager;
        public CompanyConnectionCacheService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        private string BuildKey(long companyId)
           => $"{companyId}-connection-string";

        public bool TryGetConnectionStringEntry(long companyId,out string connectionString)
        {
            return _cacheManager.TryGetValue<string>(BuildKey(companyId), out connectionString);
        }

        public void SetConnectionStringEntry(long companyId,string connectionString)
        {
            _cacheManager.SetValue<string>(BuildKey(companyId), connectionString);
        }
    }
}
