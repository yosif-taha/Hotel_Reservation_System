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

        public async Task<bool> CheckAvailabilityAsync(Guid roomId, DateTime checkIn, DateTime checkOut)
        {
            /*
            Room availability is determined by DATE OVERLAP, not by a simple flag.
            Rules:
            - A reservation blocks the room only if it is ACTIVE (Pending / Confirmed)
            - Cancelled or past reservations are ignored
            - Checkout date is NOT a staying day (room becomes available on checkout day)

            Overlap condition:
            newCheckIn < existingCheckOut    AND    newCheckOut > existingCheckIn

            Examples:
            Existing reservation: 1–11 → 5–11
            ✔ New reservation: 5–11 → 7–11   (Allowed — checkout day is free)
            ❌ New reservation: 2–11 → 6–11   (Rejected — overlapping stay)
            ❌ New reservation: 1–11 → 2–11   (Rejected — overlapping night)

            This logic ensures:
            - No double booking
            - Correct handling of edge cases
            - Industry-standard hotel reservation behavior
            */
            var hasConflict = await _context.ReservationRooms
                .AnyAsync(rr => rr.RoomId == roomId &&
                    // Only active reservations block availability
                    (rr.Reservation.Status == ReservationStatus.Pending || rr.Reservation.Status == ReservationStatus.Confirmed) &&
                    // Ignore past reservations
                    rr.Reservation.CheckOutDate > DateTime.UtcNow.Date &&
                    // Date overlap logic
                    // newCheckIn < existingCheckOut AND newCheckOut > existingCheckIn
                    checkIn < rr.Reservation.CheckOutDate && checkOut > rr.Reservation.CheckInDate
                );

            return !hasConflict;
        }
        public async Task<bool> AreRoomsAvailableAsync(IEnumerable<Guid> roomIds, DateTime checkIn, DateTime checkOut)
        {
            return !await _context.ReservationRooms.AnyAsync(rr => roomIds.Contains(rr.RoomId) &&
                    rr.Reservation.Status != ReservationStatus.Cancelled && checkIn < rr.Reservation.CheckOutDate && checkOut > rr.Reservation.CheckInDate
                );
        }
        public async Task<decimal> CalculateTotalPriceAsync(IEnumerable<Guid> roomIds, int numberOfNights)
        {
            return await _context.Rooms.Where(r => roomIds.Contains(r.Id)).SumAsync(r => r.PricePerNight * numberOfNights);
        }

    }
}
