using Hotel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Contracts
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        IQueryable<T?> GetById(Guid id);
        Task AddAsync(T entity);
        void Update(T entity, params string[] modifiedParams);
        void SoftDelete(Guid id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);


    }
}
