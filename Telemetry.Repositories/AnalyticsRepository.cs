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
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly TelemetryapiContext context;
        public AnalyticsRepository(TelemetryapiContext _context)
        {
            context = _context;
        }
        public Analytics GetAnalytics(int industryId, int[] stationIds, int[] sensorIds, DateTime from, DateTime to, int bucketMinutes)
        {
            if (from > to)
                throw new ArgumentException("The 'from' date cannot be after the 'to' date.");

            if ((to - from).TotalDays > 31)
                throw new ArgumentException("Date range cannot exceed 1 month (31 days).");

            var industry = context.Industries.FirstOrDefault(e => e.Id == industryId);
            if (industry == null)
                throw new ArgumentException($"Industry with ID {industryId} not found.");

            var validStations = context.Stations
                .Where(s => s.IndustryId == industryId && stationIds.Contains(s.Id))
                .ToList();

            if (validStations.Count != stationIds.Length)
                throw new ArgumentException("One or more station IDs are invalid or do not belong to the given industry.");

            var validStationIds = validStations.Select(s => s.Id).ToList();

            var validSensors = context.Sensors
                .Where(s => sensorIds.Contains(s.Id) && validStationIds.Contains(s.StationId))
                .ToList();

            if (validSensors.Count != sensorIds.Length)
                throw new ArgumentException("One or more sensor IDs are invalid or do not belong to the given stations.");

            var validSensorIds = validSensors.Select(s => s.Id).ToList();

            //var sensorData = context.SensorData
            //    .Where(sd => validSensorIds.Contains(sd.SensorId.Value)
            //                 && sd.Timestamp >= from && sd.Timestamp <= to)
            //    .ToList();

            
            var aggregatedSensorData = context.SensorData
    .Where(sd => validSensorIds.Contains(sd.SensorId.Value)
                 && sd.Timestamp >= from && sd.Timestamp <= to)
    .AsEnumerable() 
    .Select(sd => new
    {
        SensorId = sd.SensorId.Value,
        Value = sd.Value,
        Bucket = RoundToBucket(sd.Timestamp, bucketMinutes)
    })
    .GroupBy(x => new { x.Bucket, x.SensorId })
    .Select(g => new AggregatedSensorData
    {
        Timestamp = g.Key.Bucket,
        SensorId = g.Key.SensorId,
        Value = g.Average(x => x.Value)
    })
    .ToList();



            var analytics =PivotSensorData(aggregatedSensorData, validSensors, validStations);
            return analytics;
        }

        DateTime RoundToBucket(DateTime timestamp, int minutes)
        {
            var totalMinutes = (timestamp - DateTime.MinValue).TotalMinutes;
            var roundedMinutes = Math.Floor(totalMinutes / minutes) * minutes;
            return DateTime.MinValue.AddMinutes(roundedMinutes).ToUniversalTime();
        }

        public Analytics PivotSensorData(List<AggregatedSensorData> sensorData, List<Telemetry.Data.Models.Sensor> sensors, List<Station> stations)
        {
            // Step 1: Create map for sensorId → descriptive column name
            var stationLookup = stations.ToDictionary(s => s.Id, s => s.Name);
            var sensorLookup = sensors.ToDictionary(s => s.Id, s => new {
                SensorName = s.Name,
                StationName = stationLookup[s.StationId]
            });

            var columnMap = new Dictionary<int, string>(); // SensorId → ColumnName
            foreach (var sensor in sensors)
            {
                var key = sensor.Id;
                var value = $"{sensorLookup[key].StationName}-{sensorLookup[key].SensorName}";
                columnMap[key] = value;
            }

            // Step 2: Group by timestamp
            var groupedByTime = sensorData
                .GroupBy(d => d.Timestamp)
                .OrderBy(g => g.Key)
                .ToList();

            // Step 3: Build header row
            var columns = new List<string> { "Timestamp" };
            columns.AddRange(columnMap.Values.Distinct());

            // Step 4: Build rows
            var rows = new List<List<object>>();

            foreach (var group in groupedByTime)
            {
                var row = new Dictionary<string, object>();
                row["Timestamp"] = group.Key;

                foreach (var reading in group)
                {
                    var colName = columnMap[reading.SensorId];
                    row[colName] = reading.Value;
                }

                // Ensure order matches `columns`
                var orderedRow = columns.Select(col => row.ContainsKey(col) ? row[col] : null).ToList();
                rows.Add(orderedRow);
            }

            return new Analytics
            {
                Columns = columns,
                Rows = rows
            };
        }

    }
}
