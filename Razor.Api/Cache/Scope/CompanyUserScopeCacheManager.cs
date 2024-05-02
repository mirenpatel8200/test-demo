using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Razor.Api.Context;
using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.Web.Config;

namespace Razor.Api.Web.Cache.Scope
{
    public class CompanyUserScopeCacheManager : CompanyScopeCacheManager, ICompanyUserScopeCacheManager
    {
        public CompanyUserScopeCacheManager(IExecContext execContext, IMemoryCache cache, CacheConfig cacheConfig, ILogger<CacheManager> logger) : base(execContext,cache, cacheConfig, logger)
        {
           base.Prefix = $"{base.Prefix}-${execContext.Data.UserId}";
        }

        public string GetCompanyUserPrefix() => Prefix;
    }
}
