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
    
    public partial class Materii
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materii()
        {
            this.ProfesorMateries = new HashSet<ProfesorMaterie>();
        }
    
        public int ID { get; set; }
        public string Denumire { get; set; }
        public int Nr_Ore { get; set; }
        public int SpecializareID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfesorMaterie> ProfesorMateries { get; set; }
        public virtual Specializari Specializari { get; set; }
    }
}
