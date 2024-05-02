using System.Linq;
using Microsoft.Extensions.Logging;
using Razor.Api.DataAccess.Connection;
using Razor.Api.DataAccess.Repository.Base;

namespace Razor.Api.DataAccess.Repository.Service.Company.Impl
{
    public class CompanyDataRepository : ConfigRepositoryBase, ICompanyRepository
    {
        public CompanyDataRepository(IDbConnectionFactory connectionFactory, ILogger<ConfigRepositoryBase> logger) : base(connectionFactory, logger)
        {
           
        }

        public bool IsApiAccessEnabled(long companyId)
             => ExecQuery<bool?>("select top(1) 1 from F_COMPANY_SETTING where APIACCESSENABLED = 1 AND COMPANY_ID=" + companyId).FirstOrDefault() ?? false;
          
    }
}
