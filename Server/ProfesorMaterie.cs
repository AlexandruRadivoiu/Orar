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
    
    public partial class ProfesorMaterie
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProfesorMaterie()
        {
            this.Orars = new HashSet<Orar>();
        }
    
        public int ID { get; set; }
        public int ProfesorID { get; set; }
        public int MaterieID { get; set; }
    
        public virtual Materii Materii { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Orar> Orars { get; set; }
        public virtual Profesori Profesori { get; set; }
    }
}