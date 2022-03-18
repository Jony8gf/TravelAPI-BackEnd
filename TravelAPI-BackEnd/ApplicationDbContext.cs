using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TravelAPI_BackEnd.Entidades;

namespace TravelAPI_BackEnd
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViajePromocion>()
                .HasKey(x => new { x.PromocionId, x.ViajeId });
            modelBuilder.Entity<ViajeTipoActividad>()
                .HasKey(x => new { x.ViajeId, x.TipoActividadId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TipoActividad> TipoActividades { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<ViajePromocion> ViajesPromociones { get; set; }
        public DbSet<ViajeTipoActividad> ViajesTipoActividades { get; set; }
    }
}
