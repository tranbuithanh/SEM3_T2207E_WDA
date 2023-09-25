using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebBuyCar.Models;
using WebBuyCar.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebBuyCar.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BannerController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _dataContext.Banners.OrderByDescending(p => p.Id).ToListAsync());
        }


        /* Create */
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BannerModel banner)
        {
            ModelState.Remove("imageBanner");
            ModelState.Remove("TitleBanner");
            ModelState.Remove("desBanner");
            ModelState.Remove("imageUpload");
            ModelState.Remove("ImageBanner");
            ModelState.Remove("Status");

            if (ModelState.IsValid)
            {

                var whereBanner = await _dataContext.Banners.Where(b => b.Status == 1).ToListAsync();
                    if (banner.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/banner");

                        // random ten images
                        string imageName = Guid.NewGuid().ToString() + "_" + banner.ImageUpload.FileName;

                        // upload hinh anh vao file path
                        string filePath = Path.Combine(uploadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await banner.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        banner.ImageBanner = imageName;

                    }
                
                _dataContext.Add(banner);
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
            return View(banner);
        }

        /* edit */

        public async Task<IActionResult> Edit(int Id)
        {
            BannerModel banner = await _dataContext.Banners.FindAsync(Id);
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, BannerModel banner)
        {
            ModelState.Remove("imageBanner");
            ModelState.Remove("TitleBanner");
            ModelState.Remove("desBanner");
            ModelState.Remove("imageUpload");
            ModelState.Remove("ImageBanner");
            ModelState.Remove("Status");
            if (ModelState.IsValid)
            {
                    if (banner.ImageUpload != null)
                    {
                        // vao wwwroot lay file anh
                        string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/banner");

                        // random ten images
                        string imageName = Guid.NewGuid().ToString() + "_" + banner.ImageUpload.FileName;

                        // upload hinh anh vao file path
                        string filePath = Path.Combine(uploadDir, imageName);

                        FileStream fs = new FileStream(filePath, FileMode.Create);
                        await banner.ImageUpload.CopyToAsync(fs);
                        fs.Close();
                        banner.ImageBanner = imageName;

                    }
                
                _dataContext.Update(banner);
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
            return View(banner);
        }

        /* delete */

        public async Task<IActionResult> Delete(int Id)
        {
            BannerModel banner = await _dataContext.Banners.FindAsync(Id);
            if (!string.Equals(banner.ImageBanner, "noname.jpg"))
            {
                // vao wwwroot lay file anh
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/banner");

                // upload hinh anh vao file path
                string OldFileImage = Path.Combine(uploadDir, banner.ImageBanner);
                if (System.IO.File.Exists(OldFileImage))
                {
                    System.IO.File.Delete(OldFileImage);
                }

            }
            _dataContext.Banners.Remove(banner);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

