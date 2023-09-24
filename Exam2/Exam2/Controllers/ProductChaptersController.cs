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
    public class ProductChaptersController : Controller
    {
        private readonly ProductDbContext _context;

        public ProductChaptersController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: ProductChapters
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.ProductChapter.Include(p => p.Product);
            return View(await productDbContext.ToListAsync());
        }

        // GET: ProductChapters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductChapter == null)
            {
                return NotFound();
            }

            var productChapter = await _context.ProductChapter
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productChapter == null)
            {
                return NotFound();
            }

            return View(productChapter);
        }

        // GET: ProductChapters/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name");
            return View();
        }

        // POST: ProductChapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ProductId")] ProductChapter productChapter)
        {
            _context.Add(productChapter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductChapters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductChapter == null)
            {
                return NotFound();
            }

            var productChapter = await _context.ProductChapter.FindAsync(id);
            if (productChapter == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Name", productChapter.ProductId);
            return View(productChapter);
        }

        // POST: ProductChapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ProductId")] ProductChapter productChapter)
        {
            if (id != productChapter.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(productChapter);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductChapterExists(productChapter.Id))
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

        // GET: ProductChapters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductChapter == null)
            {
                return NotFound();
            }

            var productChapter = await _context.ProductChapter
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productChapter == null)
            {
                return NotFound();
            }

            return View(productChapter);
        }

        // POST: ProductChapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductChapter == null)
            {
                return Problem("Entity set 'ProductDbContext.ProductChapter'  is null.");
            }
            var productChapter = await _context.ProductChapter.FindAsync(id);
            if (productChapter != null)
            {
                _context.ProductChapter.Remove(productChapter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductChapterExists(int id)
        {
          return (_context.ProductChapter?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
