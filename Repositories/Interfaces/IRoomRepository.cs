using HackathanChecker.Models;

namespace HackathanChecker.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        Task<List<RoomType>> GetRoomTypesByHotelId(int hotelId);

        Task AddRoomTypeAsync(RoomType roomType);

        Task<List<Room>> GetRoomsByHotelId(int hotelId);

        Task AddRoomAsync(Room room);

        Task<List<Booking>> GetBookingsByHotelId(int hotelId);

        Task SaveAsync();
    }
}
