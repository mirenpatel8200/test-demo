using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Razor.Api.Model.V1
{
    public class CompanyTokenRequestModel
    {
        [Required]
        [JsonPropertyName("CompanyId")]
        public long CompanyId { get; set; }

        [Required]
        [JsonPropertyName("UserId")]
        public long UserId { get; set; }
    }
}
