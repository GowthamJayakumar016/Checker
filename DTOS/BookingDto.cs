namespace HackathanChecker.DTOS
{
    public class BookingDto
    {
        public int RoomId { get; set; }
        public int UserId { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    }
}
