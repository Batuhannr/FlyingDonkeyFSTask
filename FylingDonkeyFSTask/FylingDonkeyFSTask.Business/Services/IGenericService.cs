using FylingDonkeyFSTask.Common.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Business.Services
{
    public interface IGenericService<TEntity>
     where TEntity : class, ITable, new()
    {

        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression = null);
        IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression = null, List<string> includes = null);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task<TEntity> GetByIdAsync(int id);
        int EntityCount();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        Task<int> SaveChangeAsync();
    }
}
