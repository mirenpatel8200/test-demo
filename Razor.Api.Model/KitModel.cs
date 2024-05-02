using System.ComponentModel.DataAnnotations;

namespace Razor.Api.Model.V1
{
    public class KitModel
    {
        public long KitId { get; set; }
        public long KitInventoryId { get; set; }
        public string KitUid { get; set; }
        public long KitSkuId { get; set; }
        public string KitLongDescr { get; set; }
        public string KitDescr { get; set; }
        public bool IsQualified { get; set; }
        public bool IsUnique { get; set; }
        public decimal Price { get; set; }
        public int QtyOnHand { get; set; }
        public int QtyAvailable { get; set; }
        [Required]
        public string ItemMasterNumber { get; set; }
        public long ItemMasterId { get; set; }
        public string ItemMasterTitle { get; set; }
        public string ItemMasterQualifielTitle { get; set; }
        public long ManufacturerId { get; set; }
        public string ManufacturerCd { get; set; }
        public int ConditionId { get; set; }
        public string CnditionCd { get; set; }
        public string ConditionDesc { get; set; }
        public string ItemTypeCd { get; set; }
        public string ItemTypeDesc { get; set; }
        public string ItemMasterPrimaryCategoryName { get; set; }
    }
}
