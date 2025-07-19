using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Models;
using Telemetry.Repositories.Interfaces;

namespace Telemetry.Repositories
{
    public class KeyRepository : IKeyRepository
    {
        private readonly TelemetryapiContext context;
        public KeyRepository(TelemetryapiContext _context)
        {
            context = _context;            
        }
        public string? GetPrivateKey(string authToken)
        {
            if (string.IsNullOrWhiteSpace(authToken))
                return null;

            return context.Keys
                .Where(k => k.AuthKey == authToken)
                .Select(k => k.PrivateKey)
                .FirstOrDefault();
        }

    }
}
