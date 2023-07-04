using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Razor.Api.Context;
using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.Web.Config;

namespace Razor.Api.Web.Cache.Scope
{
    public class CompanyScopeCacheManager : CacheManager, ICompanyScopeCacheManager
    {
        public CompanyScopeCacheManager(IExecContext execContext, IMemoryCache cache, CacheConfig cacheConfig, ILogger<CacheManager> logger) : base(cache, cacheConfig, logger)
        {
            base.Prefix = execContext.Data.CompanyId.ToString();
        }

        public string GetCompanyPrefix() => Prefix;
    }
}
