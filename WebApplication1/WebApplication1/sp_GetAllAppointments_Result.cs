//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    
    public partial class sp_GetAllAppointments_Result
    {
        public int appointment_id { get; set; }
        public Nullable<System.DateTime> AdmitDate { get; set; }
        public string patient_id { get; set; }
        public Nullable<int> treatment_id { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<System.DateTime> appointmentDate { get; set; }
        public Nullable<System.TimeSpan> appointmentTime { get; set; }
        public Nullable<bool> isApproved { get; set; }
    }
}