namespace HackathanChecker.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        public int HotelId { get; set; }

        public string TypeName { get; set; }
        public decimal Price { get; set; }
        public int TotalRooms { get; set; }
    }
}
