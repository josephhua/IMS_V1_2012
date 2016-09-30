using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace IMS_V1
{
    [Table ("User")]
    [MetadataTypeAttribute(typeof(User.UserMetadata))]
    public partial class User
    {
        internal sealed class UserMetadata
        {
            public int User_id { get; set; }
            [Required(ErrorMessage = "Please enter a username.", AllowEmptyStrings = false)]
            public string UserName { get; set; }
            [Required(ErrorMessage = "Please enter a password.", AllowEmptyStrings = false)]
            [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
            public string Password { get; set; }
            public Nullable<int> UserType_Id { get; set; }
            public Nullable<bool> ResetPassword { get; set; }
            public string EmailAddress { get; set; }
            public string CreateAPlusImport_MarineShooting { get; set; }

            public UserType UserType { get; set; }
        }

        
    }
}