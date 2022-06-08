using System;
using System.Configuration;
using FogasiNaploAsztaliAlkalmazas.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FogasiNaploAsztaliAlkalmazas.Models
{
    public partial class Fogasi_naploContext : DbContext
    {
        public Fogasi_naploContext()
        {
        }

        public Fogasi_naploContext(DbContextOptions<Fogasi_naploContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Felhasznalok> felhasznalok { get; set; }
        public virtual DbSet<Fogasok> fogasok { get; set; }
        public virtual DbSet<Helyszinek> helyszinek { get; set; }
        public virtual DbSet<Szerepkorok> szerepkorok { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = string.Empty;
                if (RunEnvironment.IsTestEnvironment)
                {
                    connectionString = "server=localhost;user id=root;database=fogasi_naplo_teszt";
                    
                }
                else { connectionString = ConfigurationManager.ConnectionStrings["FNDB"].ConnectionString; }

                optionsBuilder.UseMySql(connectionString, ServerVersion.Parse("10.4.17-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<Felhasznalok>(entity =>
            {
                entity.HasKey(e => e.azonosito)
                    .HasName("PRIMARY");

                entity.Property(e => e.azonosito).ValueGeneratedNever();

                entity.HasOne(d => d.szerepkor)
                    .WithMany(p => p.felhasznalok)
                    .HasForeignKey(d => d.szerepkorID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("felhasznalok_ibfk_1");
            });

            modelBuilder.Entity<Fogasok>(entity =>
            {
                entity.HasKey(e => e.fogasID)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.azonositoNavigation)
                    .WithMany(p => p.fogasok)
                    .HasForeignKey(d => d.azonosito)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fogasok_ibfk_1");

                entity.HasOne(d => d.helyszin)
                    .WithMany(p => p.fogasok)
                    .HasForeignKey(d => d.helyszinID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fogasok_ibfk_2");
            });

            modelBuilder.Entity<Helyszinek>(entity =>
            {
                entity.HasKey(e => e.helyszinID)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Szerepkorok>(entity =>
            {
                entity.HasKey(e => e.szerepkorID)
                    .HasName("PRIMARY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
