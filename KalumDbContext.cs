using Microsoft.EntityFrameworkCore;
using WebApiKalum.Entities;

namespace WebApiKalum
{
    public class KalumDbContext : DbContext
    {
        public DbSet<CarreraTecnica> CarreraTecnica { get; set; }
        public DbSet<Jornada> Jornada { get; set; }
        public DbSet<Aspirante> Aspirante { get; set; }
        public DbSet<ExamenAdmision> ExamenAdmision { get; set; }
        public DbSet<Incripcion> Inscripcion { get; set; }
        public DbSet<Alumno> Alumno { get; set;}
        public DbSet<Cargo> Cargo { get; set; }
        public KalumDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CarreraTecnica>().ToTable("CarreraTecnica").HasKey(ct => new {ct.CarreraId});
            modelBuilder.Entity<Jornada>().ToTable("Jornada").HasKey(j => new {j.JornadaId});
            modelBuilder.Entity<Aspirante>().ToTable("Aspirante").HasKey(a => new {a.NoExpediente});
            modelBuilder.Entity<ExamenAdmision>().ToTable("ExamenAdmision").HasKey(ea => new {ea.ExamenId});
            modelBuilder.Entity<Incripcion>().ToTable("Inscripcion").HasKey(i => new {i.InscripcionId});
            modelBuilder.Entity<Alumno>().ToTable("Alumno").HasKey(a => new {a.Carne});
            modelBuilder.Entity<Cargo>().ToTable("Cargo").HasKey(c => new {c.CargoId});
            modelBuilder.Entity<CuentaporCobrar>().ToTable("CuentaPoeCobrar").HasKey(cp => new {cp.Cargo, cp.Anio, cp.Carne});

            modelBuilder.Entity<Aspirante>()
            .HasOne<CarreraTecnica>(c => c.CarreraTecnica)
            .WithMany(ct => ct.Aspirantes)
            .HasForeignKey(c => c.CarreraId);

            modelBuilder.Entity<Aspirante>()
            .HasOne<Jornada>(a => a.Jornada)
            .WithMany(j => j.Aspirantes)
            .HasForeignKey(a => a.JornadaId);

            modelBuilder.Entity<Aspirante>()
            .HasOne<ExamenAdmision>(a => a.ExamenAdmision)
            .WithMany(ea => ea.Aspirantes)
            .HasForeignKey(a => a.ExamenId);

            modelBuilder.Entity<Incripcion>()
            .HasOne<CarreraTecnica>(i => i.CarreraTecnica)
            .WithMany(ct => ct.Inscripciones)
            .HasForeignKey( i => i.CarreraId);

            modelBuilder.Entity<Incripcion>()
            .HasOne<Jornada>(j => j.Jornada)
            .WithMany(j => j.Inscripciones)
            .HasForeignKey( i => i.JornadaId);

            modelBuilder.Entity<Incripcion>()
            .HasOne<Alumno>(i => i.Alumno)
            .WithMany(a => a.Inscripciones)
            .HasForeignKey(i => i.Carne);
        }
    }
}