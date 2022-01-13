using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Domain.Dtos
{
    public class ContactInfoDto: IDto
    {
        public ContactType ContactType { get; set; }
        public string Info { get; set; }
    }
}
