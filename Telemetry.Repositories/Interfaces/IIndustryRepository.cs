using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Dtos;
using Telemetry.Data.Models;

namespace Telemetry.Repositories.Interfaces
{
    public interface IIndustryRepository
    {
        void create(IndustryDto industry);
        void update(IndustryDto industry);
        void delete(int id);
        List<Industry> getAll();
        Industry get(int id);
    }
}
