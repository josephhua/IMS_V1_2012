using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS_V1.Models
{
    public class ItemAttributeModel
    {
        public int ItemAttribute_Id { get; set; }
        public Nullable<int> Item_Id { get; set; }
        public Nullable<int> AttributeLookup_Id { get; set; }
        public string ActualAttributeValue { get; set; }
        public Nullable<int> AddedBy { get; set; }
        public Nullable<System.DateTime> AddedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public Attribute_Lookup Attribute_Lookup { get; set; }
        public AttributeType AttributeType { get; set; }
        public CategoryAttribute CategoryAttributes { get; set; }
        public Item Item { get; set; }
    }
}