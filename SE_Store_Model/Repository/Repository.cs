using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mysqlx.Session;
using Org.BouncyCastle.Asn1;
using SE_Store_Model.EF;
using SE_Store_Model.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SE_Store_Model.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected StoreContext _context = null;
        protected DbSet<TEntity> table = null;

        public Repository(StoreContext context = null)
        {
            if (context == null)
            {
                this._context = new StoreContext();
                table = this._context.Set<TEntity>();
            }
            else
            {
                this._context = context;
                table = _context.Set<TEntity>();
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var list = await table.ToListAsync();
            return list;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await table.FindAsync(id);
            if (entity == null)
                return null;

            _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {;
            table.Add(entity);
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            table.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> InsertListAsync(List<TEntity> listEntity)
        {
            table.AddRange(listEntity);
            return listEntity;
        }

        public async Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = table;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
    }
}
