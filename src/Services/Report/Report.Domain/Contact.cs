using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Domain
{
    public class Contact
    {
        public ContactType ContactType { get; set; }
        public string ContactInfo { get; set; }
    }

    public enum ContactType
    {
        Phone = 1,
        Email = 2,
        Location = 3
    }
}
