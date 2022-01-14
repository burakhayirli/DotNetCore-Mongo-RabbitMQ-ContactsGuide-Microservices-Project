using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Report.Domain
{
    public class ContactInfo
    {
        public string Id { get; set; }
        public ContactType ContactType { get; set; }
        public string Info { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ContactType
    {
        Phone = 1,
        Email = 2,
        Location = 3
    }
}
