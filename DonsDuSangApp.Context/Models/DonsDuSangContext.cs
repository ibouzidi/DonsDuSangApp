using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DonsDuSangApp.Context.Models;

public partial class DonsDuSangContext : DbContext
{
    public DonsDuSangContext()
    {
    }

    public DonsDuSangContext(DbContextOptions<DonsDuSangContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Donneur> Donneurs { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Questionnaire> Questionnaires { get; set; }

    public virtual DbSet<Reponse> Reponses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAP-ETC41PR\\SQLEXPRESS;Database=DonsDuSang;Integrated Security=SSPI;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Donneur>(entity =>
        {
            entity.HasKey(e => e.IdDonneur).HasName("PK__Donneur__82A042839C591B87");

            entity.ToTable("Donneur");

            entity.HasIndex(e => e.DateNaissance, "UQ__Donneur__06656525C5D9B4B8").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Donneur__AB6E61640E2035F9").IsUnique();

            entity.Property(e => e.IdDonneur)
                .ValueGeneratedNever()
                .HasColumnName("idDonneur");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Motdepasse)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("motdepasse");
            entity.Property(e => e.Nom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nom");
            entity.Property(e => e.Prenom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prenom");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.HasKey(e => e.IdQuestion).HasName("PK__Question__1196F465C80BD65D");

            entity.ToTable("Question");

            entity.Property(e => e.IdQuestion)
                .ValueGeneratedNever()
                .HasColumnName("idQuestion");
            entity.Property(e => e.Categorie)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Enonce)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.EstCritique).HasColumnName("estCritique");
        });

        modelBuilder.Entity<Questionnaire>(entity =>
        {
            entity.HasKey(e => e.IdQuestionnaire).HasName("PK__Question__609470D245F98FAD");

            entity.ToTable("Questionnaire");

            entity.Property(e => e.IdQuestionnaire)
                .ValueGeneratedNever()
                .HasColumnName("idQuestionnaire");
            entity.Property(e => e.IdDonneur).HasColumnName("idDonneur");

            entity.HasOne(d => d.IdDonneurNavigation).WithMany(p => p.Questionnaires)
                .HasForeignKey(d => d.IdDonneur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Questionn__idDon__3B75D760");
        });

        modelBuilder.Entity<Reponse>(entity =>
        {
            entity.HasKey(e => e.IdReponse).HasName("PK__Reponse__41D6459EAFCFB9DB");

            entity.ToTable("Reponse");

            entity.Property(e => e.IdReponse)
                .ValueGeneratedNever()
                .HasColumnName("idReponse");
            entity.Property(e => e.ComplementTextuel)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.IdDonneur).HasColumnName("idDonneur");
            entity.Property(e => e.IdQuestion).HasColumnName("idQuestion");
            entity.Property(e => e.Reponse1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Reponse");

            entity.HasOne(d => d.IdDonneurNavigation).WithMany(p => p.Reponses)
                .HasForeignKey(d => d.IdDonneur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reponse__idDonne__403A8C7D");

            entity.HasOne(d => d.IdQuestionNavigation).WithMany(p => p.Reponses)
                .HasForeignKey(d => d.IdQuestion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reponse__idQuest__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
