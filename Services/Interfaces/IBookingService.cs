using HackathanChecker.DTOS;
using HackathanChecker.Models;

namespace HackathanChecker.Services.Interfaces
{
    public interface IBookingService
    {
        Task<string> BookRoomAsync(BookingDto dto);
        Task<List<Booking>> GetUserBookings(int userId);

    }

}
