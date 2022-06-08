using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fogasi_naplo_webapi.Models
{
    public partial class FogasiNaploContext : DbContext
    {
        public FogasiNaploContext()
        {
        }

        public FogasiNaploContext(DbContextOptions<FogasiNaploContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Felhasznalo> felhasznalok { get; set; }
        public virtual DbSet<Fogas> fogasok { get; set; }
        public virtual DbSet<Helyszin> helyszinek { get; set; }
        public virtual DbSet<Szerepkor> szerepkorok { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;user id=root;database=fogasi_naplo",
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.21-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_general_ci");

            modelBuilder.Entity<Felhasznalo>(entity =>
            {
                entity.HasKey(e => e.azonosito)
                    .HasName("PRIMARY");

                entity.Property(e => e.azonosito).ValueGeneratedNever();

                entity.HasOne(d => d.szerepkor)
                    .WithMany(p => p.felhasznalok)
                    .HasForeignKey(d => d.szerepkorID)
                    .HasConstraintName("felhasznalok_ibfk_1");
            });

            modelBuilder.Entity<Fogas>(entity =>
            {
                entity.HasKey(e => e.fogasID)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.azonositoNavigation)
                    .WithMany(p => p.fogasok)
                    .HasForeignKey(d => d.azonosito)
                    .HasConstraintName("fogasok_ibfk_1");

                entity.HasOne(d => d.helyszin)
                    .WithMany(p => p.fogasok)
                    .HasForeignKey(d => d.helyszinID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fogasok_ibfk_2");
            });

            modelBuilder.Entity<Helyszin>(entity =>
            {
                entity.HasKey(e => e.helyszinID)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<Szerepkor>(entity =>
            {
                entity.HasKey(e => e.szerepkorID)
                    .HasName("PRIMARY");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
