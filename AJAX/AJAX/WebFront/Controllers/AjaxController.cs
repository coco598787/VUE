using Microsoft.AspNetCore.Mvc;
using WebFront.Models;

namespace WebFront.Controllers
{
    public class AjaxController : Controller
    {
        NorthwindContext _context;
        public AjaxController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: Ajax/Greet
        [HttpGet]
        public string Greet(string Name)
        {
            Thread.Sleep(3000);
            return $"Hello: {Name}!";
        }

        //POST: Ajax/PostGreet
        [HttpPost]
        public string PostGreet(string Name)
        {
            return $"Hello: {Name}!";
        }

        //POST Ajax/FetchPostGreet
        [HttpPost]
        public string FetchPostGreet(Parameter p)
        {
            Thread.Sleep(3000);
            return $"Hello: {p.Name}";
        }

        //POST: Ajax/CheckName
        [HttpPost]
        public string CheckName(string FirstName)
        {
            bool Exists=_context.Employees.Any(e => e.FirstName == FirstName);
            return Exists ? "true" : "false";
        }

        //POST: Ajax/FetchCheckName
        [HttpPost]
        public string FetchCheckName(Parameter p)
        {
            bool Exists = _context.Employees.Any(e => e.FirstName == p.Name);
            return Exists ? "true" : "false";
        }
    }
}
