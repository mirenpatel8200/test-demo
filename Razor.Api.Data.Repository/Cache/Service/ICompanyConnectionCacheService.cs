using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.DataAccess.Cache.Service
{
    public interface ICompanyConnectionCacheService
    {
        bool TryGetConnectionStringEntry(long companyId,out string connectionString);

        void SetConnectionStringEntry(long companyId,string connectionString);
    }
}
