
using Hotel.Persistence.Data.Contexts;

namespace Hotel_Reservation_System.Web.Middlewares
{
    public class TransactionMiddleware : IMiddleware
    {
        private readonly AppDbContext _context;
        public TransactionMiddleware(AppDbContext context)
        {
            _context = context;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await next(context);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
