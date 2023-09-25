using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBuyCar.Repository;
using WebBuyCar.Models;
using Microsoft.AspNetCore.Hosting;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBuyCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CategoryController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Categories.OrderByDescending(p => p.Id).ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryModel category)
        {
            ModelState.Remove("nameCategory");
            ModelState.Remove("description");
            ModelState.Remove("imageCategory");
            ModelState.Remove("slug");
            ModelState.Remove("imageUpload");

            if (ModelState.IsValid)
            {
                category.Slug = category.NameCategory.Replace(" ", "_");
                var slug = await _dataContext.Categories.FirstOrDefaultAsync(c => c.Slug == category.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "category has in database");
                    return View(category);
                }
                else
                {
                    if (category.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/categories");

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
                _dataContext.Add(category);
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

        /* edit */

        public async Task<IActionResult> Edit(int Id)
        {
            CategoryModel category = await _dataContext.Categories.FindAsync(Id);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, CategoryModel category)
        {
            ModelState.Remove("nameCategory");
            ModelState.Remove("description");
            ModelState.Remove("imageCategory");
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
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/categories");

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
            CategoryModel category = await _dataContext.Categories.FindAsync(Id);
            if (!string.Equals(category.ImageCategory, "noname.jpg"))
            {
                // vao wwwroot lay file anh
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/categories");

                // upload hinh anh vao file path
                string OldFileImage = Path.Combine(uploadDir, category.ImageCategory);
                if (System.IO.File.Exists(OldFileImage))
                {
                    System.IO.File.Delete(OldFileImage);
                }

            }
            _dataContext.Categories.Remove(category);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

