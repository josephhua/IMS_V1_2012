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
    
    public partial class User
    {
        public int User_id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Nullable<int> UserType_Id { get; set; }
        public Nullable<bool> ResetPassword { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Buyer_Number { get; set; }
        public string CreateAPlusImport_MarineShooting { get; set; }
        public byte[] EncryptPwd { get; set; }
    
        public virtual UserType UserType { get; set; }
    }
}
