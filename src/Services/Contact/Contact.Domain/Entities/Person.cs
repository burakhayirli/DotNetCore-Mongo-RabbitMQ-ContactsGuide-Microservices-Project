using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
    }
}
