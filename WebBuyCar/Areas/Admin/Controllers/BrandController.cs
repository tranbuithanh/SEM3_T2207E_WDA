using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBuyCar.Models;
using WebBuyCar.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBuyCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BrandController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult>Index()
        {
            return View(await _dataContext.Brands.OrderByDescending(b => b.Id).ToListAsync());
        }


        /* Creat */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandModel brand)
        {
            ModelState.Remove("nameBrands");
            ModelState.Remove("description");
            ModelState.Remove("imageBrand");
            ModelState.Remove("slug");
            ModelState.Remove("imageUpload");

            if (ModelState.IsValid)
            {
                brand.Slug = brand.NameBrands.Replace(" ", "_");
                var slug = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Slug == brand.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "brand has in database");
                    return View(brand);
                }
                else
                {
                    if (brand.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/brands");

                        // random ten images
                        string imageName = Guid.NewGuid().ToString() + "_" + brand.ImageUpload.FileName;

                        // upload hinh anh vao file path
                        string filePath = Path.Combine(uploadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await brand.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        brand.ImageBrand = imageName;

                    }
                }
                _dataContext.Add(brand);
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
            return View(brand);
        }

        /* edit */

        public async Task<IActionResult> Edit(int Id)
        {
            BrandModel brand = await _dataContext.Brands.FindAsync(Id);
            return View(brand);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, BrandModel brand)
        {
            ModelState.Remove("nameBrands");
            ModelState.Remove("description");
            ModelState.Remove("imageBrand");
            ModelState.Remove("slug");
            ModelState.Remove("imageUpload");
            if (ModelState.IsValid)
            {
                brand.Slug = brand.NameBrands.Replace(" ", "-");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Slug == brand.Slug);
                /* kiem tra trong database co trung slug ko */
                if (slug != null)
                {
                    ModelState.AddModelError("", "product has in database");
                    return View(brand);
                }
                else
                {
                    if (brand.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/brands");

                        // random ten images
                        string imageName = Guid.NewGuid().ToString() + "_" + brand.ImageUpload.FileName;

                        // upload hinh anh vao file path
                        string filePath = Path.Combine(uploadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await brand.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        brand.ImageBrand = imageName;

                    }
                }
                _dataContext.Update(brand);
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
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, CategoryModel category)
        {
            ModelState.Remove("nameCategory");
            ModelState.Remove("description");
            ModelState.Remove("imageBrands");
            ModelState.Remove("slug");
            ModelState.Remove("imageUpload");
            if (ModelState.IsValid)
            {
                category.Slug = category.NameCategory.Replace(" ", "-");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(p => p.Slug == category.Slug);
                /* kiem tra trong database co trung slug ko */
                if (slug != null)
                {
                    ModelState.AddModelError("", "product has in database");
                    return View(category);
                }
                else
                {
                    if (category.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/brands");

                        // random ten images
                        string imageName = Guid.NewGuid().ToString() + "_" + category.ImageUpload.FileName;

                        // upload hinh anh vao file path
                        string filePath = Path.Combine(uploadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await category.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        category.ImageCategory = imageName;

                    }
                }
                _dataContext.Update(category);
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
            return View(category);
        }

        /* delete */

        public async Task<IActionResult> Delete(int Id)
        {
            BrandModel brand = await _dataContext.Brands.FindAsync(Id);
            if (!string.Equals(brand.ImageBrand, "noname.jpg"))
            {
                // vao wwwroot lay file anh
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/brands");

                // upload hinh anh vao file path
                string OldFileImage = Path.Combine(uploadDir, brand.ImageBrand);
                if (System.IO.File.Exists(OldFileImage))
                {
                    System.IO.File.Delete(OldFileImage);
                }

            }
            _dataContext.Brands.Remove(brand);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}

