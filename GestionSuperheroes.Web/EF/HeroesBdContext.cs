using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionSuperheroes.Web.EF;

public partial class HeroesBdContext : DbContext
{
    public HeroesBdContext()
    {
    }

    public HeroesBdContext(DbContextOptions<HeroesBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Superheroe> Superheroes { get; set; }

    public virtual DbSet<Universo> Universos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MAXI-PC\\SQLSERVER2024;Database=HeroesBD;Trusted_Connection=True; Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Superheroe>(entity =>
        {
            entity.HasKey(e => e.IdSuperheroe);

            entity.ToTable("Superheroe");

            entity.Property(e => e.NombreSuperheroe).HasMaxLength(50);

            entity.HasOne(d => d.IdUniversoNavigation).WithMany(p => p.Superheroes)
                .HasForeignKey(d => d.IdUniverso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Superheroe_Universo");
        });

        modelBuilder.Entity<Universo>(entity =>
        {
            entity.HasKey(e => e.IdUniverso);

            entity.ToTable("Universo");

            entity.Property(e => e.NombreUniverso).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
