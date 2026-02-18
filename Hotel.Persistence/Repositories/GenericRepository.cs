using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class GenericRepository<T>(AppDbContext _context) : IGenericRepository<T> where T : BaseEntity
    {
        public IQueryable<T> GetAll()
        {
          var result = _context.Set<T>().AsQueryable();
            return result;
        }

        public  IQueryable<T?> GetById(Guid id)
        {
         var result = _context.Set<T>().Where(x => x.Id == id);
           return result;
        }

        public async Task AddAsync(T entity)
        {
            await  _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity, params string[] modifiedParams)
        {
            var local = _context.Set<T>().Local
                .FirstOrDefault(x => x.Id == entity.Id);

            EntityEntry entry;
            if (local == null)
            {
                _context.Set<T>().Attach(entity);
                entry = _context.Entry(entity);
            }
            else
            {
                entry = _context.Entry(local);
            }

            foreach (var propName in modifiedParams)
            {
                var value = entity.GetType()
                                  .GetProperty(propName)!
                                  .GetValue(entity);

                entry.Property(propName).CurrentValue = value;
                entry.Property(propName).IsModified = true;
            }
        }

        public void SoftDelete(Guid id)
        {
            var local = _context.Set<T>().Local
                .FirstOrDefault(x => x.Id == id);

            EntityEntry entry;
            if (local == null)
            {
                var entity = Activator.CreateInstance<T>(); // Create a new instance of T 
                entity.Id= id;

                _context.Set<T>().Attach(entity);
                entry = _context.Entry(entity);
            }
            else
            {
                entry = _context.Entry(local);
            }
            entry.Property(nameof(BaseEntity.IsDeleted)).CurrentValue = true;
            entry.Property(nameof(BaseEntity.IsDeleted)).IsModified = true;

        }
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }



    }
}
