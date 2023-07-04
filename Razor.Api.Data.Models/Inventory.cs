
using System;

namespace Razor.Api.Data.Models
{
    public class Inventory
    {
        public int ItemMasterId { get; set; }
        public string ItemMasterNumber { get; set; }
        public string ItemMasterNumberCompletion { get; set; }
        public int ItemMasterEbayCategoryId { get; set; }
        public string ItemMasterTitle { get; set; }
        public string ItemMasterQualifielTitle { get; set; }
        public string ItemMasterDesc { get; set; }
        public decimal ItemMasterLength { get; set; }
        public decimal ItemMasterWidth { get; set; }
        public decimal ItemMasterHeight { get; set; }
        public decimal ItemMasterWeight { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerCd { get; set; }
        public string ManufacturerDesc { get; set; }
        public int InventoryId { get; set; }
        public string InventorySerial { get; set; }
        public string InventoryUnique { get; set; }
        public decimal InventoryReceivedPrice { get; set; }
        public int InventoryReceivedQty { get; set; }
        public string InventoryNotes { get; set; }
        public decimal InventoryWeight { get; set; }
        public bool InventoryIsConsignment { get; set; }
        public int InventoryLocationId { get; set; }
        public string InventoryLocationName { get; set; }
        public int InventoryCustomerId { get; set; }
        public string InventoryCustomerName { get; set; }
        public int InventoryStatusId { get; set; }
        public string InventoryStatusCd { get; set; }
        public string InventoryStatusDesc { get; set; }
        public int ConditionId { get; set; }
        public string ConditionCd { get; set; }
        public string ConditionDesc { get; set; }
        public int ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeCd { get; set; }
        public string ItemTypeDesc { get; set; }
        public int ItemMasterPrimaryCategoryId { get; set; }
        public string ItemMasterPrimaryCategoryName { get; set; }
        public int SoRepUserId { get; set; }
        public string SoRepUserName { get; set; }
        public int SoCustomerId { get; set; }
        public string SoCustomerName { get; set; }
        public int PoRepUserId { get; set; }
        public string PoRepUserName { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public InventoryCapability[] InventoryCapabilities { get; set; }
        public Substitute[] Substitute { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime SoldDate { get; set; }
        public DateTime QualifiedDate { get; set; }
    }
}
