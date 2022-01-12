using ContactGuide.Shared.Utilities.Results;
using MongoDB.Bson;
using MongoDB.Driver;
using Report.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity, string> where TEntity : IEntity
    {
        private readonly IDbContext<TEntity> context;

        public BaseRepository(IDbContext<TEntity> context)
        {
            this.context = context;
        }
        public async Task<DataResult<IEnumerable<TEntity>>> GetAllAsync()
        {
            var result = await context.Entity.Find(_ => true).ToListAsync();
            return new SuccessDataResult<IEnumerable<TEntity>>(result);

        }
        public async Task<DataResult<TEntity>> GetAsync(string id)
        {
            var result = await this.context.Entity
                                     .Find(_ => _.Id == id)
                                     .FirstOrDefaultAsync();

            if (result == null)
                return new ErrorDataResult<TEntity>("Data Not Found");
            return new SuccessDataResult<TEntity>(result);
        }
        public async Task<DataResult<TEntity>> SaveAsync(TEntity item)
        {
            await this.context.Entity.InsertOneAsync(item);
            return new SuccessDataResult<TEntity>(item);

        }
        public async Task<IResult> UpdateAsync(TEntity item)
        {
            var result = await context.Entity.FindOneAndReplaceAsync(x => x.Id == item.Id, item);
            if (result != null)
                return new SuccessResult();
            return new ErrorResult("Does Not Found");
        }
        public async Task<IResult> DeleteAsync(TEntity item)
        {
            var actionResult = await this.context.Entity
                    .DeleteOneAsync(_ => _.Id == item.Id);

            if (actionResult.IsAcknowledged && actionResult.DeletedCount > 0)
                return new SuccessResult();
            return new ErrorResult();
        }
        public async Task<IResult> DeleteAllAsync()
        {
            var actionResult = await this.context.Entity.DeleteManyAsync(new BsonDocument());

            if (actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0)
                return new SuccessResult();
            return new ErrorResult();
        }
    }
}
