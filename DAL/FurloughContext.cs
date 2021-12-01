using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Furlough.Models;

namespace Furlough.DAL
{
    public partial class FurloughContext : DbContext
    {
        private readonly IConfiguration _config;

        public FurloughContext(IConfiguration config)
        {
            _config = config;
        }

        public FurloughContext(DbContextOptions<FurloughContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }

        public string GetConnection()
        {
            return _config.GetConnectionString("furloughJon");
        }

        public virtual DbSet<Models.AvailableDay> AvailableDays { get; set; } = null!;
        public virtual DbSet<Models.Department> Departments { get; set; } = null!;
        public virtual DbSet<Models.Employee> Employees { get; set; } = null!;
        public virtual DbSet<Models.Position> Positions { get; set; } = null!;
        public virtual DbSet<Models.Request> Requests { get; set; } = null!;
        public virtual DbSet<Models.RequestHistory> RequestHistories { get; set; } = null!;
        public virtual DbSet<Models.RequestStatus> RequestStatuses { get; set; } = null!;
        public virtual DbSet<Models.RequestType> RequestTypes { get; set; } = null!;
        public virtual DbSet<Models.Role> Roles { get; set; } = null!;
        public virtual DbSet<Models.SpentDaysHistory> SpentDaysHistories { get; set; } = null!;
        public virtual DbSet<Models.User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=furloughJon");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.AvailableDay>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Availabl__7AD04F115A7DCA01");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.AvailableDay)
                    .HasForeignKey<Models.AvailableDay>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Available__Emplo__114A936A");
            });

            modelBuilder.Entity<Models.Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Models.Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.Email, "UQ__Employee__A9D105344A01609C")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMPLOYEE__Depart__4222D4EF");

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMPLOYEE__Positi__412EB0B6");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EMPLOYEE__UserId__403A8C7D");
            });

            modelBuilder.Entity<Models.Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<Models.Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateUntil).HasColumnType("date");

                entity.Property(e => e.RequestedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.RequestStatus)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__Status__5812160E");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__Type__70DDC3D8");

                entity.HasOne(d => d.RequestedByNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestedByUserId)
                    .HasConstraintName("FK__Request__Request__5629CD9C");
            });

            modelBuilder.Entity<Models.RequestHistory>(entity =>
            {
                entity.ToTable("RequestHistory");

                entity.Property(e => e.AlteredOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.AlteredByNavigation)
                    .WithMany(p => p.RequestHistories)
                    .HasForeignKey(d => d.AlteredBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestHi__Alter__5DCAEF64");

                entity.HasOne(d => d.AlteredToNavigation)
                    .WithMany(p => p.RequestHistories)
                    .HasForeignKey(d => d.AlteredTo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestHi__Alter__5FB337D6");

                entity.HasOne(d => d.Request)
                    .WithMany(p => p.RequestHistories)
                    .HasForeignKey(d => d.RequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RequestHi__Reque__5CD6CB2B");
            });

            modelBuilder.Entity<Models.RequestStatus>(entity =>
            {
                entity.ToTable("RequestStatus");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<Models.RequestType>(entity =>
            {
                entity.ToTable("RequestType");

                entity.HasIndex(e => e.Type, "UQ__RequestT__F9B8A48B47E03370")
                    .IsUnique();

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Models.Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.Title, "UQ__Role__2CB664DC2655B5A7")
                    .IsUnique();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Models.SpentDaysHistory>(entity =>
            {
                entity.ToTable("SpentDaysHistory");

                entity.HasOne(d => d.RequestHistory)
                    .WithMany(p => p.SpentDaysHistories)
                    .HasForeignKey(d => d.RequestHistoryId)
                    .HasConstraintName("FK__SpentDays__Reque__17036CC0");
            });

            modelBuilder.Entity<Models.User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "UQ__User__536C85E460480D53")
                    .IsUnique();

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleId__3D5E1FD2");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.InverseUpdateByNavigation)
                    .HasForeignKey(d => d.UpdateBy)
                    .HasConstraintName("FK__User__UpdateBy__43D61337");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Furlough.Models.DepartmentRoles> DepartmentRoles { get; set; }
    }
}
