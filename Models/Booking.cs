namespace HackathanChecker.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public int RoomId { get; set; }

        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public string Status { get; set; } = "Confirmed";

        // Navigation Properties
        public Room Room { get; set; }
        public User User { get; set; }
    }
}