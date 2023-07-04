namespace Razor.Api.DataAccess.Cache.Base
{
    public interface ICompanyScopeCacheManager : ICacheManager
    {
        string GetCompanyPrefix();
    }
}
