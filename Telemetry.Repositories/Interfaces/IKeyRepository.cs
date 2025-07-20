using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Models;

namespace Telemetry.Repositories.Interfaces
{
    public interface IKeyRepository
    {
        string? GetPrivateKey(string authToken);

        Station GetStationRelatedToAuthToken(string authToken,int stationId);
        List<Sensor> GetSensorsRelatedToStation(int stationId);
    }
}
