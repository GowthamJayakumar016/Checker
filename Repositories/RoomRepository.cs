using HackathanChecker.Models;
using HackathanChecker.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class RoomRepository : IRoomRepository
{
    private readonly AppDbContext _context;

    public RoomRepository(AppDbContext context)
    {
        _context = context;
    }

    // 🔹 Get Room Types for Admin Hotel
    public async Task<List<RoomType>> GetRoomTypesByHotelId(int hotelId)
    {
        return await _context.RoomTypes
            .Where(rt => rt.HotelId == hotelId)
            .ToListAsync();
    }

    // 🔹 Add Room Type
    public async Task AddRoomTypeAsync(RoomType roomType)
    {
        await _context.RoomTypes.AddAsync(roomType);
    }

    // 🔹 Get Rooms by Hotel
    public async Task<List<Room>> GetRoomsByHotelId(int hotelId)
    {
        return await _context.Rooms
            .Include(r => r.RoomType)
            .Where(r => r.RoomType.HotelId == hotelId)
            .ToListAsync();
    }

    // 🔹 Add Room
    public async Task AddRoomAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
    }

    // 🔹 Get Bookings for Admin Hotel
    public async Task<List<Booking>> GetBookingsByHotelId(int hotelId)
    {
        return await _context.Bookings
            .Include(b => b.Room)
                .ThenInclude(r => r.RoomType)
            .Include(b => b.User)
            .Where(b => b.Room.RoomType.HotelId == hotelId)
            .ToListAsync();
    }

    // 🔹 Save Changes
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}