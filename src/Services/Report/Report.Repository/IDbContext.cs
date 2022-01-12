using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository
{
    public interface IDbContext<TEntity>
    {
        IMongoCollection<TEntity> Entity { get; }
    }
}
