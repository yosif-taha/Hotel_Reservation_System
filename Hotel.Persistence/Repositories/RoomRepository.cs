using Hotel.Domain.Contracts;
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

        public async Task<bool> CheckAvailabilityAsync(Guid id)
        {
          return await  _context.Rooms.AnyAsync(r => r.Id == id && r.IsAvailable == true);
        }
    }
}
