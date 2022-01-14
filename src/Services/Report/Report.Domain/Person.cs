using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Report.Domain
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        //[JsonConverter(typeof(JsonStringEnumConverter))]
        public List<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
    }
}
