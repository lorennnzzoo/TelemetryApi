using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Telemetry.Data.Models;

public partial class TelemetryapiContext : DbContext
{
    public TelemetryapiContext()
    {
    }

    public TelemetryapiContext(DbContextOptions<TelemetryapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Key> Keys { get; set; }

    public virtual DbSet<MonitoringType> MonitoringTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<SensorDatum> SensorData { get; set; }

    public virtual DbSet<Station> Stations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=192.168.123.72;Database=telemetryapi;Username=lorenzo;Password=lorenzo");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Category1).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Category1).HasColumnName("category");
        });

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("industry_pkey");

            entity.ToTable("industry");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.ContactEmail).HasColumnName("contact_email");
            entity.Property(e => e.ContactPerson).HasColumnName("contact_person");
            entity.Property(e => e.ContactPhone).HasColumnName("contact_phone");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Industries)
                .HasForeignKey(d => d.Category)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("industry_categoriesfkey");
        });

        modelBuilder.Entity<Key>(entity =>
        {
            entity.HasKey(e => e.AuthKey).HasName("keys_pkey");

            entity.ToTable("keys");

            entity.Property(e => e.AuthKey).HasColumnName("auth_key");
            entity.Property(e => e.IndustryId).HasColumnName("industry_id");
            entity.Property(e => e.PrivateKey).HasColumnName("private_key");
            entity.Property(e => e.PublicKey).HasColumnName("public_key");

            entity.HasOne(d => d.Industry).WithMany(p => p.Keys)
                .HasForeignKey(d => d.IndustryId)
                .HasConstraintName("keys_industryfkey");
        });

        modelBuilder.Entity<MonitoringType>(entity =>
        {
            entity.HasKey(e => e.MonitoringType1).HasName("monitoring_types_pkey");

            entity.ToTable("monitoring_types");

            entity.Property(e => e.MonitoringType1).HasColumnName("monitoring_type");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Role1).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Role1).HasColumnName("role");
            entity.Property(e => e.Description).HasColumnName("description");
        });

        modelBuilder.Entity<Sensor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sensor_pkey");

            entity.ToTable("sensor");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.InstalledDate).HasColumnName("installed_date");
            entity.Property(e => e.MeasuringUnit).HasColumnName("measuring_unit");
            entity.Property(e => e.MonitoringId).HasColumnName("monitoring_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.StationId).HasColumnName("station_id");

            entity.HasOne(d => d.Station).WithMany(p => p.Sensors)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("station_sensorfkey");
        });

        modelBuilder.Entity<SensorDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sensor_data_pkey");

            entity.ToTable("sensor_data");

            entity.HasIndex(e => new { e.SensorId, e.Timestamp }, "idx_sensor_data_sensor_time");

            entity.HasIndex(e => new { e.SensorId, e.Timestamp }, "sensor_data_sensor_id_timestamp_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SensorId).HasColumnName("sensor_id");
            entity.Property(e => e.Timestamp).HasColumnName("timestamp");
            entity.Property(e => e.Value).HasColumnName("value");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("station_pkey");

            entity.ToTable("station");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ContactEmail).HasColumnName("contact_email");
            entity.Property(e => e.ContactPerson).HasColumnName("contact_person");
            entity.Property(e => e.ContactPhone).HasColumnName("contact_phone");
            entity.Property(e => e.IndustryId).HasColumnName("industry_id");
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.MonitoringType).HasColumnName("monitoring_type");
            entity.Property(e => e.Name).HasColumnName("name");

            entity.HasOne(d => d.Industry).WithMany(p => p.Stations)
                .HasForeignKey(d => d.IndustryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("station_industryfkey");

            entity.HasOne(d => d.MonitoringTypeNavigation).WithMany(p => p.Stations)
                .HasForeignKey(d => d.MonitoringType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("station_monitoring_typesfkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email_unique").IsUnique();

            entity.HasIndex(e => e.Username, "username_unique").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FailedAttempts)
                .HasDefaultValue(0)
                .HasColumnName("failed_attempts");
            entity.Property(e => e.FullName).HasColumnName("full_name");
            entity.Property(e => e.IndustryId).HasColumnName("industry_id");
            entity.Property(e => e.LastLogin)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_login");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username).HasColumnName("username");

            entity.HasOne(d => d.Industry).WithMany(p => p.Users)
                .HasForeignKey(d => d.IndustryId)
                .HasConstraintName("user_industryidfkey");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_rolesfkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
