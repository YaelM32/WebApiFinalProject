using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.DBModels
{
    public partial class BookDBContext : DbContext
    {
        public BookDBContext()
        {
        }

        public BookDBContext(DbContextOptions<BookDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Author> Authors { get; set; } = null!;
        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Edition> Editions { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Shul> Shuls { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=srv2\\pupils;Database=BookDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.AuthorId).HasColumnName("Author_id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_id");

                entity.Property(e => e.ChipId).HasColumnName("Chip_id");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.EditionId).HasColumnName("Edition_id");

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.Property(e => e.PublishYear).HasColumnName("Publish_year");

                entity.Property(e => e.ShulId).HasColumnName("Shul_Id");

                entity.Property(e => e.VolumeNum).HasColumnName("Volume_num");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("FK_Books_Authors");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Books_Categories");

                entity.HasOne(d => d.Edition)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.EditionId)
                    .HasConstraintName("FK_Books_Editions");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Edition>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Shul>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(400);

                entity.Property(e => e.Logo).HasMaxLength(300);

                entity.Property(e => e.Map).HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(300);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.PermissionId).HasColumnName("permission_Id");

                entity.Property(e => e.ShulId).HasColumnName("shul_Id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Permission");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
