using System;
using System.Collections.Generic;
using System.Text;

namespace Razor.Api.Data.Models
{
    public class InventoryCapability
    {
        public int CapabilityTypeId { get; set; }
        public string CapabilityTypeLabel { get; set; }
        public string CapabilityValue { get; set; }
        public bool IsSkuAffecting { get; set; }
    }
}
