using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBuyCar.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBuyCar.Controllers
{
    public class ProductController : Controller
    {

        private readonly DataContext _dataContext;
        public ProductController(DataContext context)
        {
            _dataContext = context;
        }
        
        // GET: /<controller>/
        public async Task<IActionResult> Index(int Id)
        {
            if (Id == null) return RedirectToAction("Index");
            var getProductId = await _dataContext.Products.Include(p => p.Category).Where(p => p.Id == Id && p.CategoryId == p.Category.Id).OrderByDescending(p => p.Id).FirstOrDefaultAsync();
            return View(getProductId);
        }
    }
}

