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
    
    public partial class CategoryClass
    {
        public CategoryClass()
        {
            this.CategoryAttributes = new HashSet<CategoryAttribute>();
            this.Items = new HashSet<Item>();
            this.ItemDescriptionOrders = new HashSet<ItemDescriptionOrder>();
        }
    
        public int CategoryClass_Id { get; set; }
        public string Category_Id { get; set; }
        public string CategoryName { get; set; }
    
        public virtual ICollection<CategoryAttribute> CategoryAttributes { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ItemDescriptionOrder> ItemDescriptionOrders { get; set; }
    }
}