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
    
    public partial class CategoryAttribute
    {
        public int CategoryAttribute_Id { get; set; }
        public int CategoryClass_Id { get; set; }
        public Nullable<int> SubClassCode_Id { get; set; }
        public Nullable<int> FineLineCode_Id { get; set; }
        public int AttributeType_Id { get; set; }
        public Nullable<bool> Required { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual AttributeType AttributeType { get; set; }
        public virtual CategoryClass CategoryClass { get; set; }
        public virtual FineLineClass FineLineClass { get; set; }
        public virtual SubClass SubClass { get; set; }
    }
}
