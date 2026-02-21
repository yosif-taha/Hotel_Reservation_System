using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Persistence.Data.Contexts;

namespace Hotel.Persistence.Repositories
{
    public class OfferRepository(AppDbContext _context) : IOfferRepository
    {
        public async Task<bool> ExistsByIdAsync(Guid offerId)
        {
            var isExist = await _context.FindAsync<Offer>(offerId);
            if (isExist is null) return false;
            return true;
        }
    }
}
