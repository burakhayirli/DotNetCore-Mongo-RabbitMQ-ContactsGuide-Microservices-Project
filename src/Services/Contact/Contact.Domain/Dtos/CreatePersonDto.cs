using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Dtos
{
    public class CreatePersonDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ContactDto> Contacts { get; set; } = new List<ContactDto>();
    }
}
