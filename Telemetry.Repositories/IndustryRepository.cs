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
    public class IndustryRepository : IIndustryRepository
    {
        private readonly TelemetryapiContext context;
        public IndustryRepository(TelemetryapiContext _context)
        {
            context = _context;
        }
        public void create(IndustryDto industry)
        {
            context.Industries.Add(industry.CreateModel());
            context.SaveChanges();
        }

        public void delete(int id)
        {
            Industry industry = context.Industries.Where(e => e.Id == id).FirstOrDefault();
            if (industry != null)
                context.Remove(industry);
            context.SaveChanges();
        }

        public Industry get(int id)
        {
            return context.Industries.Where(e=>e.Id==id).FirstOrDefault();
        }

        public List<Industry> getAll()
        {
            return context.Industries.ToList();
        }

        public void update(IndustryDto industry)
        {
            var existing = context.Industries.Find(industry.Id);
            if (existing == null)
                throw new Exception("Sensor not found");

            // Update only what is needed
            industry.UpdateModel(existing);

            context.SaveChanges();
        }
    }
}
