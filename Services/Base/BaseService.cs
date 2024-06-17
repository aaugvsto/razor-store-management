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
    /// <summary>
    /// Base service providing CRUD operations for entities.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public abstract class BaseService<T> : IBaseService<T> where T : Entity
    {
        /// <summary>
        /// Database context.
        /// </summary>
        protected readonly DBContext context;

        /// <summary>
        /// Entity data set.
        /// </summary>
        protected DbSet<T> dbSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService{T}"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        public BaseService(DBContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <returns>A list of entities.</returns>
        public async virtual Task<IList<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        /// <summary>
        /// Gets an entity by its identifier.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <returns>The found entity or null.</returns>
        public async virtual Task<T?> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity.</returns>
        public async virtual Task<T> Add(T entity)
        {
            dbSet.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public async virtual Task Remove(T entity)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        public async Task Update(T entity)
        {
            dbSet.Update(entity);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets an entity by its identifier, including related properties.
        /// </summary>
        /// <param name="id">The entity identifier.</param>
        /// <param name="includes">The related properties to include.</param>
        /// <returns>The found entity or null.</returns>
        public async Task<T?> Get(int id, string[] includes)
        {
            var query = dbSet.AsQueryable();

            foreach (var include in includes)
                query = query.Include(include);

            return await query.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
