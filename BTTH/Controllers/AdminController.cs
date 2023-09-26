using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BTTH.Controllers
{
    public class AdminController : Controller
    {
   
        public IActionResult Index()
        {
            return View();
        }
    }
}
