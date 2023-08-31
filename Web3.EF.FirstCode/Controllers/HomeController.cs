using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Web3.EF.FirstCode.DAL;
using Web3.EF.FirstCode.Models;

namespace Web3.EF.FirstCode.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Customer> customers = new List<Customer>();
        using (var dbContext = new CustomerDbContext())
        {
            customers = dbContext.Customers.ToList();
        }
        return View(customers);
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
}

