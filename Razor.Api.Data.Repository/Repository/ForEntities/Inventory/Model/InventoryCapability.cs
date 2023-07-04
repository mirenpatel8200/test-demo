namespace Razor.Api.DataAccess.Repository.ForEntities.Inventory.Model
{
    public class InventoryCapability
    {
        public long CapabilityTypeId { get; set; }
        public string CapabilityTypeLabel { get; set; }
        public string CapabilityValue { get; set; }
        public bool IsSkuAffecting { get; set; }
    }
}
