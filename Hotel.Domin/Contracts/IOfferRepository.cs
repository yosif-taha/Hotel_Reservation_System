using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Contracts
{
    public interface IOfferRepository
    {
        Task<bool> ExistsByIdAsync(Guid offerId);
    }
}
