using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain
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
