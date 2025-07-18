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

    public virtual DbSet<Industry> Industries { get; set; }

    public virtual DbSet<Sensor> Sensors { get; set; }

    public virtual DbSet<Station> Stations { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder
        //    .HasPostgresEnum("industry_category", new[] { "Red", "Orange", "Green", "White" })
        //    .HasPostgresEnum("monitoring_type", new[] { "Ambient", "CAAQMS", "Air", "Water", "Noise", "GroundWater", "Effluent", "Emission", "ETP", "STP", "SWM", "Hazardous", "Weather", "Odour" });


        modelBuilder.HasPostgresEnum<IndustryCategory>();
        modelBuilder.HasPostgresEnum<MonitoringType>();

        modelBuilder.Entity<Industry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("industry_pkey");

            entity.ToTable("industry");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.ContactEmail).HasColumnName("contact_email");
            entity.Property(e => e.ContactPerson).HasColumnName("contact_person");
            entity.Property(e => e.ContactPhone).HasColumnName("contact_phone");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Category)
        .HasColumnName("category");

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

            entity.HasOne(e => e.Station)
                .WithMany() // or .WithMany(i => i.Stations) if you add `public ICollection<Station> Stations` in `Industry`
                .HasForeignKey(e => e.StationId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("station_sensorfkey");
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
            entity.Property(e => e.Location).HasColumnName("location");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.MonitoringType)
     .HasColumnName("monitoring_type");

            entity.Property(e => e.IndustryId).HasColumnName("industry_id");

            entity.HasOne(e => e.Industry)
                .WithMany() // or .WithMany(i => i.Stations) if you add `public ICollection<Station> Stations` in `Industry`
                .HasForeignKey(e => e.IndustryId)
                .OnDelete(DeleteBehavior.NoAction)
                .HasConstraintName("station_industryfkey");


        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
