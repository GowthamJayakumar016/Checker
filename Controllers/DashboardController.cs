using Microsoft.AspNetCore.Mvc;

namespace HackathanChecker.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult UserDashboard()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            return View();
        }
    }
}
