using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Interfaces
{
    public interface IAsyncQueryExecutor
    {
        Task<List<T>> ToListAsync<T>(IQueryable<T> query);
        Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query);
        Task<bool> AnyAsync<T>(IQueryable<T> query);
    }
}
