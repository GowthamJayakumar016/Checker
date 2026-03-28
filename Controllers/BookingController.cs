using HackathanChecker.DTOs;
using HackathanChecker.DTOS;
using HackathanChecker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HackathanChecker.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        // GET: Book page
        public IActionResult Book()
        {
            return View();
        }

        // POST: Book room
        [HttpPost]
        public async Task<IActionResult> Book(BookingDto dto)
        {
            // Get UserId from session (IMPORTANT)
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            dto.UserId = userId.Value;

            var result = await _service.BookRoomAsync(dto);

            if (result == "Success")
                return RedirectToAction("MyBookings");

            ViewBag.Error = result;
            return View("Error");
        }

        // GET: My bookings
        public async Task<IActionResult> MyBookings()
        {
            var userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Auth");

            var bookings = await _service.GetUserBookings(userId.Value);

            return View(bookings);
        }
    }
}