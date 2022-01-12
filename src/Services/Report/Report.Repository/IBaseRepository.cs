using ContactGuide.Shared.Utilities.Results;
using Report.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.Repository
{
    public interface IBaseRepository<TEntity, in TKey> where TEntity : IEntity<TKey>
    {
        Task<DataResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<DataResult<TEntity>> GetAsync(string id);
        Task<DataResult<TEntity>> SaveAsync(TEntity item);
        Task<IResult> UpdateAsync(TEntity item);
        Task<IResult> DeleteAsync(TEntity item);
        Task<IResult> DeleteAllAsync();
    }
}
