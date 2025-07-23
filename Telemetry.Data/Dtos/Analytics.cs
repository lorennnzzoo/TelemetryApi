using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemetry.Data.Dtos
{
    public class Analytics
    {
        public List<string> Columns { get; set; }
        public List<List<object>> Rows { get; set; }
    }
}
