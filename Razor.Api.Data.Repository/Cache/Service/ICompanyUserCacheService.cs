using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.DataAccess.Cache.Service
{
    public interface ICompanyUserCacheService
    {
        bool TryGetCompanyUserEnabledEntry(long companyId, long userId, out bool isEnabled);

        void SetCompanyUserEnabledEntry(long companyId, long userId, bool isEnabled);
    }
}
