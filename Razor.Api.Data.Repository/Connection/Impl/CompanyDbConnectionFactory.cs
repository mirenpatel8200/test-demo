using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using Razor.Api.DataAccess.Cache.Service;
using Razor.Api.DataAccess.CustomException;

namespace Razor.Api.DataAccess.Connection.Impl
{
    public class CompanyDbConnectionFactory : ICompanyDbConnectionFactory
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly ICompanyConnectionCacheService _companyConnectionCacheService;
        public CompanyDbConnectionFactory(IDbConnectionFactory dbConnectionFactory, ICompanyConnectionCacheService companyConnectionCacheService)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _companyConnectionCacheService = companyConnectionCacheService;
        }

        public IDbConnection GetDbConnection(long companyId)
        {
            if (!_companyConnectionCacheService.TryGetConnectionStringEntry(companyId, out var cs))
            {
                using (var c = _dbConnectionFactory.GetDbConnection())
                {
                    cs = c.Query<string>("common.sp_GET_COMPANY_CONNECTION_STRING", new { C_COMPANY_ID = companyId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    if (cs == null)
                        throw new CompanyNotFoundException("No Matching record found for the supplied company Id"); 
                }
                _companyConnectionCacheService.SetConnectionStringEntry(companyId,cs);
            }
            return new SqlConnection(cs);
        }
    }
}
