﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;
using Telemetry.Repositories.Interfaces;
using static System.Collections.Specialized.BitVector32;
using Sensor = Telemetry.Data.Models.Sensor;

namespace Telemetry.Repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly TelemetryapiContext context;
        public SensorRepository(TelemetryapiContext _context)
        {
            context = _context;
        }
        public void create(SensorDto sensor)
        {
            context.Sensors.Add(sensor.CreateModel());
            context.SaveChanges();
        }

        public void delete(int id)
        {
            var sensor = context.Sensors.FirstOrDefault(s => s.Id == id);

            if (sensor == null)
                throw new InvalidOperationException("Sensor not found.");

            context.Sensors.Remove(sensor);
            context.SaveChanges();
        }


        public Sensor get(int id)
        {
            return context.Sensors.Where(e => e.Id == id).FirstOrDefault();
        }

        public List<Sensor> getAll()
        {
            return context.Sensors.ToList();
        }

        public void update(SensorDto sensor)
        {
            var existing = context.Sensors.Find(sensor.Id);
            if (existing == null)
                throw new Exception("Sensor not found");

            // Update only what is needed
            sensor.UpdateModel(existing);

            context.SaveChanges();
        }
    }
}
