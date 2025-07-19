using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Business;
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
            
            var industryraw = industry.CreateModel();

            
            context.Industries.Add(industryraw);
            context.SaveChanges(); 

            
            string rawKey = Guid.NewGuid().ToString("N");
            rawKey = Hashing.ComputeSha256Hash(rawKey);
            Key key = new Key
            {
                IndustryId = industryraw.Id,
                AuthKey = rawKey
            };

            context.Keys.Add(key);
            context.SaveChanges();
        }

        public void delete(int id)
        {
            var industry = context.Industries
                .Include(i => i.Stations)
                .FirstOrDefault(i => i.Id == id);

            if (industry == null)
                throw new InvalidOperationException("Industry not found.");

            if (industry.Stations.Any())
                throw new InvalidOperationException("Cannot delete industry: it has linked stations.");

            context.Industries.Remove(industry);
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
