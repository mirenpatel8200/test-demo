namespace Razor.Api.DataAccess.Repository.Service.User
{
    public interface IUserRepository 
    {
        bool IsUserActive(long companyId,long userId);
    }
}
