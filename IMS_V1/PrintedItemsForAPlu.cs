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
    
    public partial class PrintedItemsForAPlu
    {
        public int Printed_Id { get; set; }
        public int Item_Id { get; set; }
        public Nullable<System.DateTime> DatePrinted { get; set; }
        public Nullable<int> PrintedBy { get; set; }
    
        public virtual Item Item { get; set; }
    }
}