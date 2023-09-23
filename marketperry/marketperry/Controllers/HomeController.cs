using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using marketperry.Models;

namespace marketperry.Controllers;

public class HomeController : Controller
{
    private readonly applicationDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger , applicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var phone = _context.mobilephones.Take(4).ToList();
        return View(phone);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
 public IActionResult pagination(int page)
    {
       int pageSize = 4;
        var phones = _context.mobilephones.ToList();
        int total = phones.Count();
        double pages = total * 1.0 / pageSize;
        pages = Math.Ceiling(pages);
        phones = phones.Skip(pageSize * (page -1)).Take(pageSize).ToList();
        return View(phones);

    }
   
    
     public IActionResult infoproduct(int? id)
    {
        if (id ==null)
        {
            return NotFound();
        }
        var mobilePhone = _context.mobilephones.FirstOrDefault(p=> p.Id == id);
        if (mobilePhone == null)
        {
            return NotFound();
        }
        return View(mobilePhone);
    }
     public IActionResult infoproductWatch(int? id)
     {
         if (id ==null)
         {
             return NotFound();
         }
         var appleWatch = _context.applewatchs.FirstOrDefault(p=> p.Id == id);
         if (appleWatch == null)
         {
             return NotFound();
         }
         return View(appleWatch);
     }
     
     public IActionResult contact()
    {
        return View();
    }

     public IActionResult IOS17()
     {
         return View();
     }
    public IActionResult Search(string query)
{
    
    var searchResults = _context.mobilephones
        .Where(p => p.PhoneName.Contains(query))
        .ToList();

    var searchResultsWatach = _context.applewatchs
        .Where(p => p.WtachName.Contains(query))
        .ToList();
    return View("searchmobile", searchResults);
}

    public IActionResult SearchWtach(string query)
    {

        var searchResultsWatach = _context.applewatchs
            .Where(p => p.WtachName.Contains(query))
            .ToList();
        return View("searchWatch", searchResultsWatach);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}