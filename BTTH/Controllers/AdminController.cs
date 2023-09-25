using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BTTH.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Teacher")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
