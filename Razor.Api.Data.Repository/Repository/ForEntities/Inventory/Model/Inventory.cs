
using System;

namespace Razor.Api.DataAccess.Repository.ForEntities.Inventory.Model
{
    public class Inventory
    {
        public long ItemMasterId { get; set; }
        public string ItemMasterNumber { get; set; }
        public string ItemMasterNumberCompletion { get; set; }
        public long ItemMasterEbayCategoryId { get; set; }
        public string ItemMasterTitle { get; set; }
        public string ItemMasterQualifielTitle { get; set; }
        public string ItemMasterDesc { get; set; }
        public decimal ItemMasterLength { get; set; }
        public decimal ItemMasterWidth { get; set; }
        public decimal ItemMasterHeight { get; set; }
        public decimal ItemMasterWeight { get; set; }
        public long ManufacturerId { get; set; }
        public string ManufacturerCd { get; set; }
        public string ManufacturerDesc { get; set; }
        public long InventoryId { get; set; }
        public string InventorySerial { get; set; }
        public string InventoryUnique { get; set; }
        public decimal InventoryReceivedPrice { get; set; }
        public int InventoryReceivedQty { get; set; }
        public string InventoryNotes { get; set; }
        public decimal InventoryWeight { get; set; }
        public bool InventoryIsConsignment { get; set; }
        public long InventoryLocationId { get; set; }
        public string InventoryLocationName { get; set; }
        public long InventoryCustomerId { get; set; }
        public string InventoryCustomerName { get; set; }
        public int InventoryStatusId { get; set; }
        public string InventoryStatusCd { get; set; }
        public string InventoryStatusDesc { get; set; }
        public int ConditionId { get; set; }
        public string ConditionCd { get; set; }
        public string ConditionDesc { get; set; }
        public long ItemId { get; set; }
        public int ItemTypeId { get; set; }
        public string ItemTypeCd { get; set; }
        public string ItemTypeDesc { get; set; }
        public long ItemMasterPrimaryCategoryId { get; set; }
        public string ItemMasterPrimaryCategoryName { get; set; }
        public long SoRepUserId { get; set; }
        public string SoRepUserName { get; set; }
        public long SoCustomerId { get; set; }
        public string SoCustomerName { get; set; }
        public long PoRepUserId { get; set; }
        public string PoRepUserName { get; set; }
        public long VendorId { get; set; }
        public string VendorName { get; set; }
        public InventoryCapability[] jsonInventoryCapabilities { get; set; }
        public Substitute[] jsonSubstitute { get; set; }
        public DateTime ReceivedDate { get; set; }
        public DateTime SoldDate { get; set; }
        public DateTime QualifiedDate { get; set; }
    }
}
