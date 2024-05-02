using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.DataAccess.Cache.Base
{
    public interface ICompanyUserScopeCacheManager : ICompanyScopeCacheManager
    {
        string GetCompanyUserPrefix();
    }
}
