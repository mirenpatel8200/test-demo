using Razor.Api.DataAccess.Cache.Base;
using Razor.Api.DataAccess.Cache.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Razor.Api.Web.Cache.Service
{
    public class CompanyUserTokenCacheService : ICompanyUserTokenCacheService
    {
        private readonly ICacheManager _cacheManager;
        public CompanyUserTokenCacheService(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        private string BuildKey(long companyId, long userId)
           => $"{companyId}-{userId}-api-access-token";

        public bool TryGetTokenEntry(long companyId, long userId, out string token)
        {
            return _cacheManager.TryGetValue<string>(BuildKey(companyId, userId), out token);
        }

        public void RemoveTokenEntry(long companyId, long userId)
        {
            _cacheManager.RemoveValue(BuildKey(companyId, userId));
        }

        public void SetTokenEntry(long companyId, long userId,string token)
        {
            _cacheManager.SetValue<string>(BuildKey(companyId, userId), token);
        }
    }
}
