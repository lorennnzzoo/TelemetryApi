using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;

namespace Telemetry.Business
{
    public interface ISensorDataRepository
    {
        void InsertSensorData(StationUploadModel model);
    }
}
