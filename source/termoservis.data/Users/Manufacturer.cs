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
    
    public partial class Manufacturer : User
    {
        public Manufacturer()
        {
            this.Device = new HashSet<Device>();
        }
    
        public string Website { get; set; }
    
        public virtual ICollection<Device> Device { get; set; }
    }
}
