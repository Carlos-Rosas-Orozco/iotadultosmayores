using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace IoT_PI.Models
{
    public partial class usersContext : DbContext
    {
        public usersContext()
        {
        }

        public usersContext(DbContextOptions<usersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdultoMayor> AdultoMayor { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-F9IBHA9\\SQLEXPRESS;Database=users;User Id=carlos; Password=1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdultoMayor>(entity =>
            {
                entity.HasKey(e => e.IdAdulto)
                    .HasName("PK__AdultoMa__25F92200C5ED37AF");

                entity.Property(e => e.IdAdulto).HasColumnName("idAdulto");

                entity.Property(e => e.DescripcionGeneral)
                    .HasColumnName("descripcion_general")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdusuarioNavigation)
                    .WithMany(p => p.AdultoMayor)
                    .HasForeignKey(d => d.Idusuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AdultoMay__idusu__534D60F1");
            });

            modelBuilder.Entity<Usuarios>(entity =>
            {
                entity.HasKey(e => e.Idusuario)
                    .HasName("PK__usuarios__080A9743EE6AF10B");

                entity.ToTable("usuarios");

                entity.HasIndex(e => e.Correo)
                    .HasName("UQ__usuarios__2A586E0B2EDC145E")
                    .IsUnique();

                entity.HasIndex(e => e.Nombre)
                    .HasName("UQ__usuarios__72AFBCC65DBA61B5")
                    .IsUnique();

                entity.Property(e => e.Idusuario).HasColumnName("idusuario");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnName("correo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasColumnName("estado")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnName("nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
