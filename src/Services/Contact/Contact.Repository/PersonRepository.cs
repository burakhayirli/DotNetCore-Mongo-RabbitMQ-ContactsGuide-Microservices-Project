using Contact.Domain.Entities;
using ContactGuide.Shared.Utilities.Results;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IDbContext<Person> _context;

        public PersonRepository(IDbContext<Person> context) : base(context)
        {
            _context = context;
        }

        public Task<IResult> DeleteContactInfo(string personId, string contactInfoId)
        {
            throw new NotImplementedException();
        }

        public async Task<IDataResult<ContactInfo>> SaveContactInfo(string personId, ContactInfo newContactInfo)
        {
            var person =  _context.Entity
                                     .Find(_ => _.Id == personId)
                                     .FirstOrDefaultAsync().Result;

            if (person == null)
                return new ErrorDataResult<ContactInfo>("Person Not Found");

            person.ContactInfos.Add(newContactInfo);
            await UpdateAsync(person);
            return new SuccessDataResult<ContactInfo>(newContactInfo);
        }

        public Task<IResult> UpdateContactInfo(string personId, ContactInfo contactInfo)
        {
            throw new NotImplementedException();
        }
    }
}
