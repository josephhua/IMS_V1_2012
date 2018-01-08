//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IMS_V1
{
    using System;
    using System.Collections.Generic;
    
    public partial class ImportedItem
    {
        public int ImportItem_id { get; set; }
        public Nullable<int> Item_id { get; set; }
        public Nullable<int> ManufacturerLogo_Id { get; set; }
        public string Item_Description { get; set; }
        public string MFG_Number { get; set; }
        public Nullable<int> UM_Id { get; set; }
        public Nullable<int> VdUM_id { get; set; }
        public Nullable<decimal> MSRP { get; set; }
        public Nullable<decimal> Level1 { get; set; }
        public Nullable<decimal> Level2 { get; set; }
        public Nullable<decimal> Level3 { get; set; }
        public Nullable<decimal> Level4 { get; set; }
        public Nullable<decimal> Level5 { get; set; }
        public string Qty_Break { get; set; }
        public Nullable<decimal> Qty_BreakPrice { get; set; }
        public int CategoryClass_Id { get; set; }
        public Nullable<int> SubClassCode_Id { get; set; }
        public Nullable<int> FineLineCode_Id { get; set; }
        public Nullable<int> CatWebCode_Id { get; set; }
        public Nullable<int> Freight_Id { get; set; }
        public string Plan_YN { get; set; }
        public Nullable<int> ABC_Id { get; set; }
        public string WareHouses { get; set; }
        public Nullable<int> STD { get; set; }
        public string MIN { get; set; }
        public Nullable<decimal> VICost { get; set; }
        public string Haz { get; set; }
        public string UPC { get; set; }
        public string Buyer { get; set; }
        public string Exclusive { get; set; }
        public string Allocated { get; set; }
        public string DropShip { get; set; }
        public string PreventFromWeb { get; set; }
        public string SpecialOrder { get; set; }
        public string WholeSaleMTP { get; set; }
        public Nullable<decimal> WholeSaleMTPPrice { get; set; }
        public string MinAdvertisePriceFlag { get; set; }
        public Nullable<decimal> MinAdvertisePrice { get; set; }
        public string Company99 { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> FileId { get; set; }
        public Nullable<int> ActionId { get; set; }
        public Nullable<int> CaliberId { get; set; }
        public Nullable<int> CapacityId { get; set; }
        public Nullable<int> FinishId { get; set; }
        public Nullable<int> BarrelLengthId { get; set; }
        public Nullable<int> ModelId { get; set; }
        public Nullable<int> MiscId { get; set; }
        public string ModelManuValue { get; set; }
        public string MiscManuValue { get; set; }
        public Nullable<int> BullettTypeId { get; set; }
        public Nullable<int> CountPerBoxId { get; set; }
        public Nullable<int> GrainWeightId { get; set; }
        public Nullable<int> FamilyNameId { get; set; }
        public Nullable<int> CountPerCaseId { get; set; }
        public Nullable<int> FeetPerSecondId { get; set; }
        public Nullable<int> FFLType_Id { get; set; }
        public string FFLCaliber { get; set; }
        public string FFLModel { get; set; }
        public string FFLMFGName { get; set; }
        public string FFLMFGImportName { get; set; }
        public string FFLGauge { get; set; }
        public Nullable<bool> Imported { get; set; }
        public Nullable<System.DateTime> ImportedDt { get; set; }
    }
}