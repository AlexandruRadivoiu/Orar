﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class FacultateEntities : DbContext
    {
        public FacultateEntities()
            : base("name=FacultateEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Departamente> Departamentes { get; set; }
        public virtual DbSet<Grupe> Grupes { get; set; }
        public virtual DbSet<Materii> Materiis { get; set; }
        public virtual DbSet<Orar> Orars { get; set; }
        public virtual DbSet<Profesori> Profesoris { get; set; }
        public virtual DbSet<ProfesorMaterie> ProfesorMateries { get; set; }
        public virtual DbSet<Specializari> Specializaris { get; set; }
        public virtual DbSet<Studenti> Studentis { get; set; }
    
        public virtual ObjectResult<getOrarByGrupaId_Result> getOrarByGrupaId(Nullable<int> grupaId)
        {
            var grupaIdParameter = grupaId.HasValue ?
                new ObjectParameter("GrupaId", grupaId) :
                new ObjectParameter("GrupaId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getOrarByGrupaId_Result>("getOrarByGrupaId", grupaIdParameter);
        }
    
        public virtual ObjectResult<getOrarByProfesor_Result> getOrarByProfesor(string profesorNume)
        {
            var profesorNumeParameter = profesorNume != null ?
                new ObjectParameter("ProfesorNume", profesorNume) :
                new ObjectParameter("ProfesorNume", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<getOrarByProfesor_Result>("getOrarByProfesor", profesorNumeParameter);
        }
    }
}
