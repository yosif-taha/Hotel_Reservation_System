using Hotel.Domain.Contracts;
using Hotel.Domain.Entities;
using Hotel.Domin.Entities.Enums;
using Hotel.Persistence.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class RoomRepository(AppDbContext _context) : IRoomRepository
    {
        public async Task<bool> ExistsByNumberAsync(int roomNumber)
        {
           return await _context.Rooms.AnyAsync(r => r.RoomNumber == roomNumber);
        }

        public async Task<bool> CheckAvailabilityAsync(Guid id, DateTime checkIn, DateTime checkOut)
        {
            var overlappingReservation = await _context.ReservationRooms.Where(rr => rr.RoomId == id)
                .Where(rr => rr.Reservation.Status != ReservationStatus.Cancelled)
                .Where(rr =>checkIn < rr.Reservation.CheckOutDate && checkOut > rr.Reservation.CheckInDate).AnyAsync();

            return !overlappingReservation;
            //return await _context.Rooms.AnyAsync(r => r.Id == id);// && r.IsAvailable == true);
        }
    }
}
