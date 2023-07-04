using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.DataAccess.Cache.Service
{
    public interface ICompanyAccessCacheService
    {
        bool TryGetApiAccessEntry(out bool isEnabled);

        void SetApiAccessEntry(bool isEnabled);
    }
}
