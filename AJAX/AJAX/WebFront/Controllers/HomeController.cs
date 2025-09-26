using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebFront.Models;

namespace WebFront.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // GET: Home/Employee
        [HttpGet]
        public IActionResult Employee()
        {
            return View();
        }

        // GET: Home/Greet
        [HttpGet]
        public IActionResult Greet()
        {
            return View();
        }

        // GET: Home/CheckName
        [HttpGet]
        public IActionResult CheckName()
        {
            return View();
        }

		// GET: Home/Basic
		[HttpGet]
		public IActionResult Basic()
		{
			return View();
		}

		// GET: Home/Rate
		[HttpGet]
		public IActionResult Rate()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
