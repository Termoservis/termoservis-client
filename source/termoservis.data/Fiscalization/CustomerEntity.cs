//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Termoservis.Data.Fiscalization
{
    using System;
    using System.Collections.Generic;
    
    public partial class CustomerEntity
    {
        public CustomerEntity()
        {
            this.AccountEntity = new HashSet<AccountEntity>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string PIN { get; set; }
        public string Address { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
    
        public virtual ICollection<AccountEntity> AccountEntity { get; set; }
    }
}
