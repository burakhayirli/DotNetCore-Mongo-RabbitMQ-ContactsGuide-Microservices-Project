using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Entities
{
    public class ContactInfo : BaseEntity
    {
        public ContactType ContactType { get; set; }
        public string Info { get; set; }
    }

    public enum ContactType
    {
        Phone = 1,
        Email = 2,
        Location = 3
    }
}
