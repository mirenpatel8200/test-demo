namespace Razor.Api.Web.Config.Api
{
    public class ApiConfig
    {
        public string          Title                     { get; set; }
        public string          GroupNameFormat           { get; set; }
        public bool            SubstituteApiVersionInUrl { get; set; }
        public ApiVersionModel Versions                  { get; set; }
    }
}
