using Contact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Repository
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly IDbContext<Person> _context;

        public PersonRepository(IDbContext<Person> context) : base(context)
        {
            _context = context;
        }
    }
}
