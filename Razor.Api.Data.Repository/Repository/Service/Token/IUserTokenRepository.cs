namespace Razor.Api.DataAccess.Repository.Service.Token
{
    public interface IUserTokenRepository
    {
        void SaveToken(long companyId, long userId, string token);
        string GetToken(long companyId, long userId);
    }
}
