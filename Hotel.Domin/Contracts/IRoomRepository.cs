using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Contracts
{
    public interface IRoomRepository
    {
        Task<bool> ExistsByNumberAsync(int roomNumber);
        Task<bool> CheckAvailabilityAsync(Guid id);
    }
}
