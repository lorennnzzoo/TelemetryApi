using Mapster;
using Microsoft.EntityFrameworkCore;
using Telemetry.Data.Models;
using Telemetry.Repositories;
using Telemetry.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Telemetry.Data.Models.TelemetryapiContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("telemetryApi"), npgsqlOptions =>
{
    npgsqlOptions.MapEnum<IndustryCategory>();
    npgsqlOptions.MapEnum<MonitoringType>();
}));

builder.Services.AddScoped<IIndustryRepository, IndustryRepository>();
builder.Services.AddScoped<IStationRepository, StationRepository>();
builder.Services.AddScoped<ISensorRepository, SensorRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
