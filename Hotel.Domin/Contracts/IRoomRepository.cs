using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Contracts
{
    public interface IRoomRepository
    {
        Task<bool> CheckRoomExistAsync(Guid roomIds);
        Task<bool> CheckRoomsExistAsync(List<Guid> roomIds);
        Task<bool> ExistsByNumberAsync(int roomNumber);
        Task<bool> CheckAvailabilityAsync(Guid id, DateOnly checkIn, DateOnly checkOut);
        Task<bool> AreRoomsAvailableAsync(IEnumerable<Guid> roomIds, DateOnly checkIn, DateOnly checkOut);
        Task<decimal> CalculateTotalPriceAsync(IEnumerable<Guid> roomIds, int numberOfNights);


    }
}
