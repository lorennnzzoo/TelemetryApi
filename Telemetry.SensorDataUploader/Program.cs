using Microsoft.EntityFrameworkCore;
using Telemetry.Business;
using Telemetry.Repositories;
using Telemetry.Repositories.Interfaces;
using Telemetry.SensorDataUploader;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<SensorDataUploader>();
builder.Services.AddDbContext<Telemetry.Data.Models.TelemetryapiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("telemetryApi")));

builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
builder.Services.AddScoped<IKeyRepository, KeyRepository>();
builder.Services.AddScoped<ISensorDataRepository, SensorDataRepository>();
var host = builder.Build();
host.Run();
