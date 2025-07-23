using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;

namespace Telemetry.Repositories.Interfaces
{
    public interface IAnalyticsRepository
    {
        Analytics GetAnalytics(int industryId, int[] stationIds, int[] sensorIds,DateTime from,DateTime to, int bucketMinutes);
    }
}
