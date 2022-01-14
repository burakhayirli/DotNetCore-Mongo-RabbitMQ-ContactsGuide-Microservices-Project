using Contact.Domain.Entities;
using ContactGuide.Shared.Utilities.Results;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Repository
{
    public interface IPersonRepository : IBaseRepository<Person, string>
    {
        //Task<IDataResult<ContactInfo>> SaveContactInfo(string personId, ContactInfo newContactInfo);
        //Task<IResult> UpdateContactInfo(string personId, ContactInfo contactInfo);
        //Task<IResult> DeleteContactInfo(string personId, string contactInfoId);
    }
}
