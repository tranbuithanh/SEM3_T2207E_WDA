using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBuyCar.Repository;
using WebBuyCar.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBuyCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {

            return View(await _dataContext.Products.OrderByDescending(p => p.Id).Include(p => p.Category).Include(p => p.Brand).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "NameCategory");
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "NameBrands");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductModel product)
        {

            /* kiem tra du lieu truyen vao de lay Id va NameCategory day len database */
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "NameCategory", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "NameBrands", product.BrandId);
            ModelState.Remove("nameCar");
            ModelState.Remove("slug");
            ModelState.Remove("description");
            ModelState.Remove("yearOfManufacture");
            ModelState.Remove("price");
            ModelState.Remove("category");
            ModelState.Remove("brand");
            ModelState.Remove("status");
            ModelState.Remove("imageProduct");
            ModelState.Remove("ImageUpload");
            if (ModelState.IsValid)
            {
                product.Slug = product.NameCar.Replace(" ", "-");
                var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                /* kiem tra trong database co trung slug ko */
                if (slug != null)
                {
                    ModelState.AddModelError("", "product has in database");
                    return View(product);
                }
                else
                {
                    if (product.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");

                        // random ten images
                        string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;

                        // upload hinh anh vao file path
                        string filePath = Path.Combine(uploadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await product.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        product.ImageProduct = imageName;

                    }
                }
                _dataContext.Add(product);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(product);
        }

        /* edit product */
        public async Task<IActionResult> Edit(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "NameCategory");
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "NameBrands");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, ProductModel product)
        {

            /* kiem tra du lieu truyen vao de lay Id va NameCategory day len database */
            ViewBag.Categories = new SelectList(_dataContext.Categories, "Id", "NameCategory", product.CategoryId);
            ViewBag.Brands = new SelectList(_dataContext.Brands, "Id", "NameBrands", product.BrandId);
            ModelState.Remove("nameCar");
            ModelState.Remove("slug");
            ModelState.Remove("description");
            ModelState.Remove("yearOfManufacture");
            ModelState.Remove("price");
            ModelState.Remove("category");
            ModelState.Remove("brand");
            ModelState.Remove("status");
            ModelState.Remove("ImageProduct");
            ModelState.Remove("ImageUpload");
            if (ModelState.IsValid)
            {
                product.Slug = product.NameCar.Replace(" ", "-");
                var slug = await _dataContext.Products.FirstOrDefaultAsync(p => p.Slug == product.Slug);
                /* kiem tra trong database co trung slug ko */
                if (slug != null)
                {
                    ModelState.AddModelError("", "product has in database");
                    return View(product);
                }
                else
                {
                    if (product.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");

                        // random ten images
                        string imageName = Guid.NewGuid().ToString() + "_" + product.ImageUpload.FileName;

                        // upload hinh anh vao file path
                        string filePath = Path.Combine(uploadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await product.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        product.ImageProduct = imageName;

                    }
                }
                _dataContext.Update(product);
                _dataContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
                string errorMessage = string.Join("\n", errors);
                return BadRequest(errorMessage);
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int Id)
        {
            ProductModel product = await _dataContext.Products.FindAsync(Id);
            if (!string.Equals(product.ImageProduct,"noname.jpg"))
            {
                // vao wwwroot lay file anh
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/products");

                // upload hinh anh vao file path
                string OldFileImage = Path.Combine(uploadDir, product.ImageProduct);
                if (System.IO.File.Exists(OldFileImage))
                {
                    System.IO.File.Delete(OldFileImage);
                }

            }
            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}

