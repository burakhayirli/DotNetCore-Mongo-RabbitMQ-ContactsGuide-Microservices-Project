using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Repository
{
    public interface IDbContext<TEntity>
    {
        IMongoCollection<TEntity> Entity { get; }
    }
}
