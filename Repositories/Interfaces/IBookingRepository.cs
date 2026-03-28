namespace HackathanChecker.Repositories.Interfaces
{
    public interface IBookingRepository
    {
        Task<bool> IsRoomAvailable(int roomId, DateTime checkIn, DateTime checkOut);
        Task AddBookingAsync(Booking booking);
        Task SaveAsync();
    }
}
