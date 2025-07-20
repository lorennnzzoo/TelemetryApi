using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;

namespace Telemetry.Repositories.Interfaces
{
    public interface ISensorRepository
    {
        void create(SensorDto sensor);
        void update(SensorDto sensor);
        void delete(int id);
        List<Data.Models.Sensor> getAll();
        Data.Models.Sensor get(int id);
    }
}
