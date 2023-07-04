namespace Razor.Api.Model.V1
{
    public class InventoryCapabilityModel
    {
        public long CapabilityTypeId { get; set; }
        public string CapabilityTypeLabel { get; set; }
        public string CapabilityValue { get; set; }
        public bool IsSkuAffecting { get; set; }
    }
}
