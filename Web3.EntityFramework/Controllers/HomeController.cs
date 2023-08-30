using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web3.EntityFramework.Models;

namespace Web3.EntityFramework.Controllers
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
            return View(this.GetPeople());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private List<Person> GetPeople()
        {
            using (var context = new AdventureWorks2019Context())
            {
                return context.People.Take(100).ToList();
            }
        }
    }
}