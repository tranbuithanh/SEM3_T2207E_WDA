using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Exam2.DAL;
using Exam2.Models;

namespace Exam2.Controllers
{
    public class ProductChapterImagesController : Controller
    {
        private readonly ProductDbContext _context;

        public ProductChapterImagesController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: ProductChapterImages
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.ProductChapterImage.Include(p => p.ProductChapter);
            return View(await productDbContext.ToListAsync());
        }

        // GET: ProductChapterImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductChapterImage == null)
            {
                return NotFound();
            }

            var productChapterImage = await _context.ProductChapterImage
                .Include(p => p.ProductChapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productChapterImage == null)
            {
                return NotFound();
            }

            return View(productChapterImage);
        }

        // GET: ProductChapterImages/Create
        public IActionResult Create()
        {
            ViewData["ProductChapterId"] = new SelectList(_context.ProductChapter, "Id", "Name");
            return View();
        }

        // POST: ProductChapterImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,ProductChapterId")] ProductChapterImage productChapterImage)
        {
            _context.Add(productChapterImage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductChapterImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductChapterImage == null)
            {
                return NotFound();
            }

            var productChapterImage = await _context.ProductChapterImage.FindAsync(id);
            if (productChapterImage == null)
            {
                return NotFound();
            }
            ViewData["ProductChapterId"] = new SelectList(_context.ProductChapter, "Id", "Name", productChapterImage.ProductChapterId);
            return View(productChapterImage);
        }

        // POST: ProductChapterImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image,ProductChapterId")] ProductChapterImage productChapterImage)
        {
            if (id != productChapterImage.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(productChapterImage);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductChapterImageExists(productChapterImage.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductChapterImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductChapterImage == null)
            {
                return NotFound();
            }

            var productChapterImage = await _context.ProductChapterImage
                .Include(p => p.ProductChapter)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productChapterImage == null)
            {
                return NotFound();
            }

            return View(productChapterImage);
        }

        // POST: ProductChapterImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductChapterImage == null)
            {
                return Problem("Entity set 'ProductDbContext.ProductChapterImage'  is null.");
            }
            var productChapterImage = await _context.ProductChapterImage.FindAsync(id);
            if (productChapterImage != null)
            {
                _context.ProductChapterImage.Remove(productChapterImage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductChapterImageExists(int id)
        {
          return (_context.ProductChapterImage?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
