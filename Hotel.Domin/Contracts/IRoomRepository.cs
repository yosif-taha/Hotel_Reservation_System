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
        Task<bool> CheckAvailabilityAsync(Guid id, DateTime checkIn, DateTime checkOut);
        Task<bool> AreRoomsAvailableAsync(IEnumerable<Guid> roomIds, DateTime checkIn, DateTime checkOut);
        Task<decimal> CalculateTotalPriceAsync(IEnumerable<Guid> roomIds, int numberOfNights);


    }
}
