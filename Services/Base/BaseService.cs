using DataAccess;
using Domain.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Base
{
    public abstract class BaseService<T> : IBaseService<T> where T : Entity
    {
        protected readonly DBContext context;

        protected DbSet<T> dbSet;

        public BaseService(DBContext context) 
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public async virtual Task<IList<T>> GetAll() 
        {
            return await dbSet.ToListAsync();
        }

        public async virtual Task<T?> Get(int id) 
        {
            return await dbSet.FindAsync(id);
        }

        public async virtual Task<T> Add(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async virtual Task Remove(T entity)
        {
            dbSet.Attach(entity);
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task<T?> Get(int id, string[] includes)
        {
            var query = dbSet.AsQueryable();

            foreach(var include in includes)
                query = query.Include(include);

            return query.Single(x => x.Id == id);
        }
    }
}
