using HackathanChecker.Models;
using HackathanChecker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

public class AdminController : Controller
{
    private readonly IRoomRepository _roomRepo;
    private readonly AppDbContext _context;

    public AdminController(IRoomRepository roomRepo, AppDbContext context)
    {
        _roomRepo = roomRepo;
        _context = context;
    }

    // 🔹 Dashboard
    public IActionResult Dashboard()
    {
        var role = HttpContext.Session.GetString("Role");

        if (role != "Admin")
            return RedirectToAction("Login", "Auth");

        return View("AdminDashboard");
    }

    // 🔹 ROOM TYPES
    public async Task<IActionResult> RoomTypes()
    {
        int hotelId = GetAdminHotelId();

        var roomTypes = await _roomRepo.GetRoomTypesByHotelId(hotelId);

        return View(roomTypes);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoomType(RoomType model)
    {
        int hotelId = GetAdminHotelId();

        model.HotelId = hotelId;

        await _roomRepo.AddRoomTypeAsync(model);
        await _roomRepo.SaveAsync();

        return RedirectToAction("RoomTypes");
    }

    // 🔹 ROOMS
    public async Task<IActionResult> Rooms()
    {
        int hotelId = GetAdminHotelId();

        var rooms = await _roomRepo.GetRoomsByHotelId(hotelId);

        return View(rooms);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom(Room model)
    {
        await _roomRepo.AddRoomAsync(model);
        await _roomRepo.SaveAsync();

        return RedirectToAction("Rooms");
    }

    // 🔹 BOOKINGS
    public async Task<IActionResult> Bookings()
    {
        int hotelId = GetAdminHotelId();

        var bookings = await _roomRepo.GetBookingsByHotelId(hotelId);

        return View(bookings);
    }

    // 🔹 CANCEL BOOKING
    public async Task<IActionResult> CancelBooking(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking != null)
        {
            booking.Status = "Cancelled";
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Bookings");
    }

    // 🔹 Helper Method
    private int GetAdminHotelId()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        var admin = _context.Users.FirstOrDefault(u => u.Id == userId);

        return admin?.HotelId ?? 0;
    }
}