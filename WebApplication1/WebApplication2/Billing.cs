//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Billing
    {
        public string bill_number { get; set; }
        public Nullable<int> patient_id { get; set; }
        public Nullable<int> consumable_id { get; set; }
        public Nullable<int> userId { get; set; }
    
        public virtual Consumable Consumable { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual User User { get; set; }
    }
}
