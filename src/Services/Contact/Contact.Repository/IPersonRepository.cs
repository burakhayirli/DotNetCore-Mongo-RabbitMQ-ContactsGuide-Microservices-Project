using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Repository
{
    public interface IPersonRepository : IBaseRepository<Person, string>
    {
    }
}
