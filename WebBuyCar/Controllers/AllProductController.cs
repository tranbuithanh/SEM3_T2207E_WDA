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
    public class AllProductController : Controller
    {
        private readonly DataContext _dataContext;
        public AllProductController(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["BrandId"] = new SelectList(_dataContext.Brands, "Id", "NameBrands");
            var selectYear = _dataContext.Products.Select(y => y.YearOfManufacture).Distinct().ToList();
            ViewData["Year"] = new SelectList(selectYear);

            var getProduct = await _dataContext.Products.OrderByDescending(p => p.Id).ToListAsync();
            return View(getProduct);
        }
        [HttpPost]

        public async Task<IActionResult> Index(int Brands, int Year)
        {
            ViewData["BrandId"] = new SelectList(_dataContext.Brands, "Id", "NameBrands");
            var selectYear = _dataContext.Products.Select(y => y.YearOfManufacture).Distinct().ToList();
            ViewData["Year"] = new SelectList(selectYear);
            var products = await _dataContext.Products.Include(p => p.Brand).Where(b => b.BrandId == Brands && b.YearOfManufacture == Year).ToListAsync();
            return View(products);
        }
    }
}

