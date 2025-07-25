﻿using Microsoft.EntityFrameworkCore;
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

        public Station GetStationRelatedToAuthToken(string authToken,int stationId)
        {
            if (string.IsNullOrWhiteSpace(authToken))
                return null;

            Key key = context.Keys
                            .Where(k => k.AuthKey == authToken).FirstOrDefault();

            if (key == null)
                return null;


            int industryId = key.IndustryId;
            



            var station = context.Stations
                .FirstOrDefault(s => s.IndustryId == industryId && s.Id == stationId);

            return station;
        }


        public List<Sensor> GetSensorsRelatedToStation(int stationId)
        {
            return context.Sensors.Where(e => e.StationId == stationId).ToList();
        }
    }
}
