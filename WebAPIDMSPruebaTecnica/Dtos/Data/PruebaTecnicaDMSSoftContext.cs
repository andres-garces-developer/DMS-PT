using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebAPIDMSPruebaTecnica.Dtos.Models;

namespace WebAPIDMSPruebaTecnica.Dtos.Data
{
    public partial class PruebaTecnicaDMSSoftContext : DbContext
    {
        public PruebaTecnicaDMSSoftContext()
        {
        }

        public PruebaTecnicaDMSSoftContext(DbContextOptions<PruebaTecnicaDMSSoftContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaulfConnectDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.HasKey(e => e.IdAppointment)
                    .HasName("PK__appointm__F9CC20B72574CBC9");

                entity.ToTable("appointment");

                entity.Property(e => e.IdAppointment).HasColumnName("id_appointment");

                entity.Property(e => e.DescriptionAppointment)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("description_appointment");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("PK__employee__F807679CA40DD640");

                entity.ToTable("employee");

                entity.Property(e => e.IdEmployee).HasColumnName("id_employee");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.ConfirmEmail).HasColumnName("confirm_email");

                entity.Property(e => e.DateContratation)
                    .HasColumnType("date")
                    .HasColumnName("date_contratation");

                entity.Property(e => e.EmailEmployee)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email_employee");

                entity.Property(e => e.FkTypeAppointment).HasColumnName("fk_type_appointment");

                entity.Property(e => e.LastnameEmployee)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastname_employee");

                entity.Property(e => e.NameEmployee)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name_employee");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.FkTypeAppointmentNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.FkTypeAppointment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("employee_fk_appointment");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
