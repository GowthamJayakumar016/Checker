using HackathanChecker.DTOS;
using HackathanChecker.Repositories.Interfaces;
using HackathanChecker.Services.Interfaces;

namespace HackathanChecker.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _repo;
        private readonly AppDbContext _context;

        public BookingService(IBookingRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<string> BookRoomAsync(BookingDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var available = await _repo.IsRoomAvailable(dto.RoomId, dto.CheckIn, dto.CheckOut);

                if (!available)
                    return "Room not available";

                var booking = new Booking
                {
                    RoomId = dto.RoomId,
                    UserId = dto.UserId,
                    CheckIn = dto.CheckIn,
                    CheckOut = dto.CheckOut,
                    Status = "Confirmed"
                };

                await _repo.AddBookingAsync(booking);
                await _repo.SaveAsync();

                await transaction.CommitAsync();

                return "Success";
            }
            catch
            {
                await transaction.RollbackAsync();
                return "Error";
            }
        }
    }
}
