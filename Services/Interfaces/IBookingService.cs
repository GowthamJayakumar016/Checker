using HackathanChecker.DTOS;

namespace HackathanChecker.Services.Interfaces
{
    public interface IBookingService
    {
        Task<string> BookRoomAsync(BookingDto dto);
    }
}
