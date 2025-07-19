using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemetry.Data.Models;

namespace Telemetry.Data.Dtos
{
    public class IndustryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string ContactPerson { get; set; } = null!;

        public string ContactPhone { get; set; } = null!;

        public string ContactEmail { get; set; } = null!;

        public string Address { get; set; } = null!;
        public IndustryCategories Category { get; set; }

        public Industry CreateModel()
        {
            return new Industry
            {
                Id = this.Id,
                Name = this.Name,
                ContactPerson = this.ContactPerson,
                ContactPhone = this.ContactPhone,
                ContactEmail = this.ContactEmail,
                Address = this.Address,
                Category = this.Category.ToString()
            };
        }
        public void UpdateModel(Industry industry)
        {
            industry.Name = this.Name;
            industry.ContactPerson = this.ContactPerson;
            industry.ContactPhone = this.ContactPhone;
            industry.ContactEmail = this.ContactEmail;
            industry.Address = this.Address;
            industry.Category = this.Category.ToString();
        }

    }
}
