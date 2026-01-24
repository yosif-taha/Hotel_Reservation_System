using Hotel.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Executor
{
    public class EfAsyncQueryExecutor : IAsyncQueryExecutor
    {
        public Task<List<T>> ToListAsync<T>(IQueryable<T> query)
         => query.ToListAsync();

        public Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query)
            => query.FirstOrDefaultAsync();

        public Task<bool> AnyAsync<T>(IQueryable<T> query)
            => query.AnyAsync();
    }
}
