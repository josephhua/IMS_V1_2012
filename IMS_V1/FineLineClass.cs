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
    
    public partial class FineLineClass
    {
        public FineLineClass()
        {
            this.CategoryAttributes = new HashSet<CategoryAttribute>();
            this.ItemDescriptionOrders = new HashSet<ItemDescriptionOrder>();
            this.ItemDescriptionOrders1 = new HashSet<ItemDescriptionOrder>();
            this.Items = new HashSet<Item>();
        }
    
        public int FineLineCode_Id { get; set; }
        public string Category_Id { get; set; }
        public string SubClass_id { get; set; }
        public string FineLine_Id { get; set; }
        public string FinelineName { get; set; }
    
        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; }
        public virtual ICollection<ItemDescriptionOrder> ItemDescriptionOrders { get; set; }
        public virtual ICollection<ItemDescriptionOrder> ItemDescriptionOrders1 { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
