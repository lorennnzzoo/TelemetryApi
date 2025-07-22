using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;
using Telemetry.Repositories.Interfaces;

namespace Telemetry.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly TelemetryapiContext context;
        public DashboardRepository(TelemetryapiContext _context)
        {
            context = _context;
        }
        public IndustryDashboard GetIndustryDashboard(int id)
        {
            var industry = context.Industries.FirstOrDefault(e => e.Id == id);
            if (industry == null)
                return null;

            var stations = context.Stations.Where(e => e.IndustryId == id).ToList();
            if (!stations.Any())
                return null;

            var stationIds = stations.Select(s => s.Id).ToList();

            // Load all sensors for the stations
            var sensors = context.Sensors
                .Where(s => stationIds.Contains(s.StationId))
                .ToList();

            var sensorIds = sensors.Select(s => s.Id).ToList();

            // Load latest data per sensor
            var latestSensorData = context.SensorData
                .Where(sd => sensorIds.Contains(sd.SensorId.Value))
                .GroupBy(sd => sd.SensorId)
                .Select(group => group
                    .OrderByDescending(sd => sd.Timestamp)
                    .FirstOrDefault())
                .ToList()
                .ToDictionary(sd => sd.SensorId, sd => sd);

            var sensorsByStation = sensors
                .GroupBy(s => s.StationId)
                .ToDictionary(g => g.Key, g => g.ToList());

            var dashboard = new IndustryDashboard
            {
                Id = industry.Id,
                Name = industry.Name,
                Stations = stations.Select(station => new StationDashboard
                {
                    Id = station.Id,
                    Name = station.Name,
                    Sensors = sensorsByStation.ContainsKey(station.Id)
                        ? sensorsByStation[station.Id].Select(sensor => new SensorDashboard
                        {
                            Id = sensor.Id,
                            Name = sensor.Name,
                            Units = sensor.MeasuringUnit,
                            DataPoint = latestSensorData.ContainsKey(sensor.Id) && latestSensorData[sensor.Id] != null
                                ? new SensorDataPoint
                                {
                                    Value = latestSensorData[sensor.Id].Value,
                                    Timestamp = latestSensorData[sensor.Id].Timestamp
                                }
                                : null
                        }).ToList()
                        : new List<SensorDashboard>()
                }).ToList()
            };

            return dashboard;
        }

    }
}
