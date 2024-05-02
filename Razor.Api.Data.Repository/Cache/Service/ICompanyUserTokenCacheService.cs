using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.DataAccess.Cache.Service
{
    public interface ICompanyUserTokenCacheService
    {
        bool TryGetTokenEntry(long companyId, long userId,out string token);

        void SetTokenEntry(long companyId, long userId,string token);

        void RemoveTokenEntry(long companyId, long userId);
    }
}
