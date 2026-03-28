namespace HackathanChecker.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomNumber { get; set; }

        // Navigation property to RoomType so EF Core can include related RoomType
        public RoomType RoomType { get; set; }
    }
}
