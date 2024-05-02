using System.Data;

namespace Razor.Api.DataAccess.Connection
{
    public interface IDbConnectionFactory
    {
        IDbConnection GetDbConnection();
    }
}