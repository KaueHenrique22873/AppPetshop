using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AppPetshop.Models;

public partial class PetshopContext : DbContext
{
    public PetshopContext()
    {
    }

    public PetshopContext(DbContextOptions<PetshopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agendamento> Agendamentos { get; set; }

    public virtual DbSet<Pet> Pets { get; set; }

    public virtual DbSet<Servico> Servicos { get; set; }

    public virtual DbSet<Tutor> Tutors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConexaoSqlServer");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agendamento>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Agendamento");

            entity.Property(e => e.IdServico).HasColumnName("idServico");
            entity.Property(e => e.IdTutor).HasColumnName("idTutor");

            entity.HasOne(d => d.IdServicoNavigation).WithMany(p => p.Agendamentos)
                .HasForeignKey(d => d.IdServico)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agendamento_Servico");

            entity.HasOne(d => d.IdTutorNavigation).WithMany(p => p.Agendamentos)
                .HasForeignKey(d => d.IdTutor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Agendamento_Tutor");
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Pet");

            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servico>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Servico");

            entity.Property(e => e.Descricao)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tutor>(entity =>
        {
            entity.HasKey(e => e.Codigo);

            entity.ToTable("Tutor");

            entity.Property(e => e.Idpet).HasColumnName("idpet");
            entity.Property(e => e.Nome)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdpetNavigation).WithMany(p => p.Tutors)
                .HasForeignKey(d => d.Idpet)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tutor_Pet");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
