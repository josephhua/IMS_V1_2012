using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMS_V1
{
    public partial class ReplacementItem
    {
        internal sealed class ReplacementItemMetadata
        {
            [DisplayAttribute(Name = "Existing Item Number")]
            public string existingItm_Num { get; set; }
        }
    }
}