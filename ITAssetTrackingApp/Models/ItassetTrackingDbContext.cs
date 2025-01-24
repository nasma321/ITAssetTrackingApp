using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ITAssetTrackingApp.Models;

public partial class ItassetTrackingDbContext : DbContext
{
    public ItassetTrackingDbContext()
    {
    }

    public ItassetTrackingDbContext(DbContextOptions<ItassetTrackingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetAssignmentHistory> AssetAssignmentHistories { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ITAssetTrackingDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AssetId).HasName("PK__Assets__4349237274B4A8B6");

            entity.HasIndex(e => e.AssetNumber, "UQ__Assets__856CE34BABA45A75").IsUnique();

            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.AssetNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AssetType).HasMaxLength(50);
            entity.Property(e => e.AssignedStaffId).HasColumnName("AssignedStaffID");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.AssignedStaff).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AssignedStaffId)
                .HasConstraintName("FK__Assets__Assigned__3E52440B");
        });

        modelBuilder.Entity<AssetAssignmentHistory>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("PK__AssetAss__32499E57C60AC07A");

            entity.ToTable("AssetAssignmentHistory");

            entity.Property(e => e.AssignmentId).HasColumnName("AssignmentID");
            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.StaffId).HasColumnName("StaffID");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetAssignmentHistories)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetAssi__Asset__412EB0B6");

            entity.HasOne(d => d.Staff).WithMany(p => p.AssetAssignmentHistories)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetAssi__Staff__4222D4EF");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__96D4AAF7FCAD30DE");

            entity.HasIndex(e => e.NationalIdcardNumber, "UQ__Staff__D9463B734B3351DB").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("StaffID");
            entity.Property(e => e.Department).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.NationalIdcardNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NationalIDCardNumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
