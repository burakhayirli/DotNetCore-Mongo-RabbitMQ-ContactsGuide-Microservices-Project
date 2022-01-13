using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Domain.Dtos
{
    public class UpdatePersonDto:IDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ContactInfoDto> Contacts { get; set; } = new List<ContactInfoDto>();
    }
}
