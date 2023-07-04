using System.Linq;
using Microsoft.Extensions.Logging;
using Razor.Api.DataAccess.Connection;
using Razor.Api.DataAccess.Repository.Base;
using Razor.Api.DataAccess.Repository.Base.Company;

namespace Razor.Api.DataAccess.Repository.Service.User.Impl
{
    public class UserDataRepository : ExplicitCompanyRepositoryBase, IUserRepository
    {
       
        public UserDataRepository(ICompanyDbConnectionFactory dbConnectionFactory, ILogger<ConfigRepositoryBase> logger) : base(dbConnectionFactory, logger)
        {
           
        }

        public bool IsUserActive(long companyId, long userId) 
            => ExecQuery<bool?>(companyId, "select top(1) 1 from tb_User where  IS_DELETED = 0 AND IS_INACTIVE=0 AND USERID =" + userId).FirstOrDefault() ?? false;

    }
}
