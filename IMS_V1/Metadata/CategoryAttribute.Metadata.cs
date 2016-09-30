using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMS_V1
{
    [Table("CategoryAttribute")]
    [MetadataTypeAttribute(typeof(CategoryAttribute.CategoryAttributeMetadata))]
    public partial class CategoryAttribute
    {
        internal sealed class CategoryAttributeMetadata
        {
            public int CategoryAttribute_Id { get; set; }
            [Required(ErrorMessage = "Please select a Category.")]
            [Display(Name = "Category")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            public int CategoryClass_Id { get; set; }

            [Display(Name = "SubClass")]
            [DisplayFormat(ConvertEmptyStringToNull = true)]
            public Nullable<int> SubClassCode_Id { get; set; }

            [Display(Name = "Fine Line")]
            [DisplayFormat(ConvertEmptyStringToNull = true)]
            public Nullable<int> FineLineCode_Id { get; set; }
            
            public int AttributeType_Id { get; set; }
            public Nullable<bool> Required { get; set; }
            public Nullable<int> ModifiedBy { get; set; }
            public Nullable<System.DateTime> ModifiedDate { get; set; }

        }
    }
}