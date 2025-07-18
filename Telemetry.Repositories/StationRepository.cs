using Microsoft.EntityFrameworkCore;
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
    public class StationRepository : IStationRepository
    {
        private readonly TelemetryapiContext context;
        public StationRepository(TelemetryapiContext _context)
        {
            context = _context;
        }
        public void create(StationDto station)
        {
            context.Stations.Add(station.CreateModel());
            context.SaveChanges();
        }

        public void delete(int id)
        {
            Station station = context.Stations.Where(e => e.Id == id).FirstOrDefault();
            if (station != null)
                context.Remove(station);
            context.SaveChanges();
        }

        public Station get(int id)
        {
            return context.Stations.Where(e => e.Id == id).FirstOrDefault();
        }

        public List<Station> getAll()
        {
            return context.Stations.ToList();
        }

        public void update(StationDto station)
        {
            var existing = context.Stations.Find(station.Id);
            if (existing == null)
                throw new Exception("Sensor not found");

            // Update only what is needed
            station.UpdateModel(existing);

            context.SaveChanges();
        }
    }
}
