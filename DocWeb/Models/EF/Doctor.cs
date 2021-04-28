//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DocWeb.Models.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Doctor
    {
        public int DoctorId { get; set; }
        public int TitleId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string HPCSANo { get; set; }
        public string IdNo { get; set; }
        public int DisciplineId { get; set; }
        public int ProvinceId { get; set; }
        public int RegionId { get; set; }
    
        public virtual Discipline Discipline { get; set; }
        public virtual Province Province { get; set; }
        public virtual Title Title { get; set; }
        public virtual Region Region { get; set; }
    }
}
