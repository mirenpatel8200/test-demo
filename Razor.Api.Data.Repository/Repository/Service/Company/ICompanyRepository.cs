namespace Razor.Api.DataAccess.Repository.Service.Company
{
    public interface ICompanyRepository
    {
        bool IsApiAccessEnabled(long companyId);
    }
}
