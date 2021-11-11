using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Furlough.Models;

namespace Furlough.DAL
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AvailableDay> AvailableDays { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<Request> Requests { get; set; } = null!;
        public virtual DbSet<RequestHistory> RequestHistories { get; set; } = null!;
        public virtual DbSet<RequestStatus> RequestStatuses { get; set; } = null!;
        public virtual DbSet<RequestType> RequestTypes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<SpentDaysHistory> SpentDaysHistories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=furloughJon");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AvailableDay>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK__Availabl__7AD04F115A7DCA01");

                entity.Property(e => e.EmployeeId).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithOne(p => p.AvailableDay)
                    .HasForeignKey<AvailableDay>(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Available__Emplo__114A936A");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.HasIndex(e => e.Email, "UQ__Employee__A9D105344A01609C")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(320)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

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

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("Position");

                entity.Property(e => e.Title).IsUnicode(false);
            });

            modelBuilder.Entity<Request>(entity =>
            {
                entity.ToTable("Request");

                entity.Property(e => e.DateFrom).HasColumnType("date");

                entity.Property(e => e.DateUntil).HasColumnType("date");

                entity.Property(e => e.RequestedOn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.RequestedByNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.RequestedBy)
                    .HasConstraintName("FK__Request__Request__5629CD9C");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__Status__5812160E");

                entity.HasOne(d => d.TypeNavigation)
                    .WithMany(p => p.Requests)
                    .HasForeignKey(d => d.Type)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Request__Type__70DDC3D8");
            });

            modelBuilder.Entity<RequestHistory>(entity =>
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

            modelBuilder.Entity<RequestStatus>(entity =>
            {
                entity.ToTable("RequestStatus");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Type).IsUnicode(false);
            });

            modelBuilder.Entity<RequestType>(entity =>
            {
                entity.ToTable("RequestType");

                entity.HasIndex(e => e.Type, "UQ__RequestT__F9B8A48B47E03370")
                    .IsUnique();

                entity.Property(e => e.Type)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.HasIndex(e => e.Title, "UQ__Role__2CB664DC2655B5A7")
                    .IsUnique();

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SpentDaysHistory>(entity =>
            {
                entity.ToTable("SpentDaysHistory");

                entity.HasOne(d => d.RequestHistory)
                    .WithMany(p => p.SpentDaysHistories)
                    .HasForeignKey(d => d.RequestHistoryId)
                    .HasConstraintName("FK__SpentDays__Reque__17036CC0");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.InsertDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__RoleId__3D5E1FD2");

                entity.HasOne(d => d.UpdateByNavigation)
                    .WithMany(p => p.InverseUpdateByNavigation)
                    .HasForeignKey(d => d.UpdateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__User__UpdateBy__02FC7413");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
