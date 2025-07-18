using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;

namespace Telemetry.Repositories.Interfaces
{
    public interface IStationRepository
    {
        void create(StationDto station);
        void update(StationDto station);
        void delete(int id);
        List<Station> getAll();
        Station get(int id);
    }
}
