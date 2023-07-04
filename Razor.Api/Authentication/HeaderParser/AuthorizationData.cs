namespace Razor.Api.Web.Authentication.HeaderParser
{
    public class AuthorizationData
    {
        public long UserId { get; set; }

        public long CompanyId { get; set; }

        public string Token { get; set; }
    }
}
