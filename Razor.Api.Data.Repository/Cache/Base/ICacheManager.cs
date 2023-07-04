namespace Razor.Api.DataAccess.Cache.Base
{
    public interface ICacheManager
    {
        bool TryGetValue<T>(string cacheKey, out T value);
        void SetValue<T>(string cacheKey, T value);
        void RemoveValue(string cacheKey);
    }
}
