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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Appointments = new HashSet<Appointment>();
            this.Billings = new HashSet<Billing>();
            this.Patients = new HashSet<Patient>();
        }
    
        public int userId { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password_ { get; set; }
        public Nullable<int> age { get; set; }
        public string contact_number { get; set; }
        public string gender { get; set; }
        public string specialization { get; set; }
        public Nullable<int> role_id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Appointment> Appointments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Billing> Billings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual Role Role { get; set; }
    }
}
