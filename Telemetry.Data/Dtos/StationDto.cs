using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Models;

namespace Telemetry.Data.Dtos
{
    public class StationDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string ContactPerson { get; set; } = null!;

        public string ContactPhone { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;
        public MonitoringTypes MonitoringType { get; set; }
        public int IndustryId { get; set; }

        public Station CreateModel()
        {
            return new Station
            {
                Id = this.Id,
                Name = this.Name,
                Location = this.Location,
                ContactPerson = this.ContactPerson,
                ContactPhone = this.ContactPhone,
                ContactEmail = this.ContactEmail,
                MonitoringType = this.MonitoringType.ToString(),
                IndustryId = this.IndustryId
            };
        }
        public void UpdateModel(Station station)
        {
            station.Name = this.Name;
            station.Location = this.Location;
            station.ContactPerson = this.ContactPerson;
            station.ContactPhone = this.ContactPhone;
            station.ContactEmail = this.ContactEmail;
            station.MonitoringType = this.MonitoringType.ToString();
            station.IndustryId = this.IndustryId;
        }

    }
}
