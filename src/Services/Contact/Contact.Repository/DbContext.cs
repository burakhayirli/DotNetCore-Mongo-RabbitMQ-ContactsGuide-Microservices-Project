using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contact.Repository
{
    public class DbContext<TEntity> : IDbContext<TEntity>
    {
        private readonly IMongoDatabase database = null;

        public DbContext(IOptions<DbConfiguration> configuration)
        {
            var client = new MongoClient(configuration.Value.ConnectionString);
            if (client != null)
            {
                this.database = client.GetDatabase(configuration.Value.Database);
            }
        }

        public IMongoCollection<TEntity> Entity
        {
            get
            {
                return this.database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }
    }
}
