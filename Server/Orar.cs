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
    
    public partial class Orar
    {
        public int ID { get; set; }
        public System.DateTime Zi { get; set; }
        public int Nr_Row { get; set; }
        public int Modul { get; set; }
        public int GrupaID { get; set; }
        public int ProfesorMaterieID { get; set; }
    
        public virtual Grupe Grupe { get; set; }
        public virtual ProfesorMaterie ProfesorMaterie { get; set; }
    }
}
