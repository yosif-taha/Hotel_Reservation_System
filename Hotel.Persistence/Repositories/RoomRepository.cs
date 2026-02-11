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

        public async Task<bool> CheckAvailabilityAsync(Guid roomId, DateOnly checkIn, DateOnly checkOut)
        {
            /*
            Room availability is determined by DATE OVERLAP, not by a simple flag.
            Rules:
            - A reservation blocks the room only if it is ACTIVE (Pending / Confirmed)
            - Cancelled or past reservations are ignored
            - Checkout date is NOT a staying day (room becomes available on checkout day)
            */

            var today = DateOnly.FromDateTime(DateTime.Now);

            var hasConflict = await _context.ReservationRooms
                .Where(rr => rr.RoomId == roomId)
                // Only active reservations block availability
                .Where(rr => rr.Reservation.Status == ReservationStatus.Pending || rr.Reservation.Status == ReservationStatus.Confirmed)
                // Ignore past reservations
                .Where(rr => rr.Reservation.CheckOutDate >= today)
                // Date overlap logic: newCheckIn < existingCheckOut AND newCheckOut > existingCheckIn
                .Where(rr => checkIn < rr.Reservation.CheckOutDate && checkOut > rr.Reservation.CheckInDate)
                .AnyAsync();

            return !hasConflict;
        }

        public async Task<bool> AreRoomsAvailableAsync(IEnumerable<Guid> roomIds, DateOnly checkIn, DateOnly checkOut)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            return !await _context.ReservationRooms
                .Where(rr => roomIds.Contains(rr.RoomId))
                .Where(rr => rr.Reservation != null)
                .Where(rr => rr.Reservation.Status != ReservationStatus.Cancelled)
                .Where(rr => rr.Reservation.CheckOutDate >= today)
                // Date overlap logic
                .Where(rr => checkIn < rr.Reservation.CheckOutDate && checkOut > rr.Reservation.CheckInDate)
                .Where(rr => rr.IsDeleted == false)
                .AnyAsync();
        }

        public async Task<decimal> CalculateTotalPriceAsync(IEnumerable<Guid> roomIds, int numberOfNights)
        {
            return await _context.Rooms.Where(r => roomIds.Contains(r.Id)).SumAsync(r => r.PricePerNight * numberOfNights);
        }

    }
}
