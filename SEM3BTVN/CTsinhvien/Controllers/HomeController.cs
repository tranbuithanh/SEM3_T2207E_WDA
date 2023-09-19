using CTsinhvien.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CTsinhvien.Controllers
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

     
        public IActionResult Test()
        {
    List<Person> list = new List<Person>();
    using (var dbContext = new Sem3MvcContext())
    {
        list = dbContext.People.ToList();
    }
    return View(list);
        }

       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}