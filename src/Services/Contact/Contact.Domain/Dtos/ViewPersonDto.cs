using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Dtos
{
    public class ViewPersonDto: IDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ContactInfoDto> ContactInfos { get; set; } = new List<ContactInfoDto>();
    }
}
