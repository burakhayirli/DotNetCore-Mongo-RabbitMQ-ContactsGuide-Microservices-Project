using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Repository
{
    public class PersonRepository : BaseRepository<Domain.Person>, IPersonRepository
    {
        private readonly IDbContext<Domain.Person> _context;

        public PersonRepository(IDbContext<Domain.Person> context) : base(context)
        {
            _context = context;
        }
    }
}
