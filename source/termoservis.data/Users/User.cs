//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Termoservis.Data.Users
{
    using System;
    using System.Collections.Generic;
    
    public abstract partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PIN { get; set; }
        public UserTypes Type { get; set; }
        public string Note { get; set; }
    
        public virtual ContactInfo ContactInfo { get; set; }
    }
}
