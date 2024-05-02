using System.Linq;
using Microsoft.Extensions.Logging;
using Razor.Api.DataAccess.Connection;
using Razor.Api.DataAccess.Repository.Base;
using Razor.Api.DataAccess.Repository.Base.Company;

namespace Razor.Api.DataAccess.Repository.Service.Token.Impl
{
    public class UserTokenDataRepository : ExplicitCompanyRepositoryBase, IUserTokenRepository
    {
       
        public UserTokenDataRepository(ICompanyDbConnectionFactory dbConnectionFactory, ILogger<ConfigRepositoryBase> logger) : base(dbConnectionFactory, logger)
        {
            
        }

        //TODO 3-2: cache the usage. By caching the usage you keep your code more flexible. Why do we cache it if we do not use Get from cache?
        public string GetToken(long companyId, long userId)
            => ExecQuery<string>(companyId, $"select API_KEY_TOKEN from tb_User where USERID = {userId}").FirstOrDefault();

        public void SaveToken(long companyId, long userId, string token)
            => Exec(companyId,
            @"update tb_User set API_KEY_TOKEN = @API_TOKEN where UserID = @USER_ID",
            new
            {
                USER_ID = userId,
                API_TOKEN = token
            });

           
         
    }
}
