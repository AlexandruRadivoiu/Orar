//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Server
{
    using System;
    using System.Collections.Generic;
    
    public partial class Profesori
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Profesori()
        {
            this.ProfesorMateries = new HashSet<ProfesorMaterie>();
        }
    
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        public string Nickname { get; set; }
        public string Parola { get; set; }
        public Nullable<System.DateTime> Data_nasterii { get; set; }
        public string Cnp { get; set; }
        public int DepartamentID { get; set; }
    
        public virtual Departamente Departamente { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfesorMaterie> ProfesorMateries { get; set; }
    }
}