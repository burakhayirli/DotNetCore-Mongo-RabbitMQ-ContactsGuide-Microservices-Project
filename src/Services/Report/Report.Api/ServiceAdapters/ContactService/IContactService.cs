using Report.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Report.Api.ServiceAdapters.ContactService
{
    public interface IContactService
    {
        Task<List<Person>> GetAll();
    }
}
