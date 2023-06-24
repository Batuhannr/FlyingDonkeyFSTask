using FylingDonkeyFSTask.Business.Services;
using FylingDonkeyFSTask.Common.Abstract;
using FylingDonkeyFSTask.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FylingDonkeyFSTask.Business.Concreate
{
    public class GenericManager<TEntity> : IGenericService<TEntity>
         where TEntity : class, ITable, new()
    {
        private readonly IGenericDal<TEntity> _genericDal;

        public GenericManager(IGenericDal<TEntity> genericDal)
        {
            _genericDal = genericDal;

        }

        public void Add(TEntity entity)
        {
            _genericDal.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _genericDal.Delete(entity);
        }

        public void Update(TEntity entity)
        {
            _genericDal.Update(entity);
        }

        public int EntityCount()
        {
            return _genericDal.EntityCount();
        }

        public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression = null)
        {
            return _genericDal.GetAllAsync(expression);
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression = null)
        {
            return _genericDal.GetQueryable(expression);
        }

        public virtual Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return _genericDal.GetAsync(expression);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _genericDal.GetByIdAsync(id);
        }

        public async Task<int> SaveChangeAsync()
        {
            return await _genericDal.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> expression = null, List<string> includes = null)
        {
            var Iq = this.GetQueryable(expression);

            if (includes != null)
            {
                foreach (var inc in includes)
                {
                    Iq = Iq.Include(inc);
                }
            }

            return Iq;
        }
    }
}
