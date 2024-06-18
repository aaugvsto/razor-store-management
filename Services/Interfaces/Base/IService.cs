using Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces.Base
{
    public interface IBaseService<T> where T : Entity
    {
        Task<IList<T>> GetAll();

        Task<T?> Get(int id);

        Task<T?> Get(int id, string[] includes);

        Task<T> Add(T entity);

        Task Remove(T entity);

        Task Update(T entity);
    }
}
