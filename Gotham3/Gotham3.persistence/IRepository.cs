using Gotham3.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gotham3.persistence
{
    public interface IRepository<T> where T : Entity
    {
        Task<IQueryable<T>> GetAll();
        Task Add(T entity);
        Task Update(T entity);
        Task<T> GetById(int? id);
        Task Delete(int? id);
        Task Publish(int id);
    }
}
