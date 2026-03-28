using Microsoft.AspNetCore.Mvc;

using HackathanChecker.DTOs;
using HackathanChecker.Services.Interfaces;

namespace HacakthanChecker.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

      
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.RegisterAsync(dto);

            if (!result)
            {
                ModelState.AddModelError("Email", "Email already exists");
                return View(dto);
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                ModelState.AddModelError("Email", "Invalid credentials");
                return View(dto);
            }

          
            HttpContext.Session.SetString("AuthToken", result.Token);

          
            HttpContext.Session.SetString("UserRole", result.Role);

         

            if (result.Role == "Admin")
                return RedirectToAction("AdminDashboard", "Dashboard");

            return RedirectToAction("UserDashboard", "Dashboard");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}