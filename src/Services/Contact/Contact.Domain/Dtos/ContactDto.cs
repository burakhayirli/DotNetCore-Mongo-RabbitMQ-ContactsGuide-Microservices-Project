using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Dtos
{
    public class ContactDto
    {
        //public string PersonId { get; set; }
        public ContactType ContactType { get; set; }
        public string ContactInfo { get; set; }
    }
}
