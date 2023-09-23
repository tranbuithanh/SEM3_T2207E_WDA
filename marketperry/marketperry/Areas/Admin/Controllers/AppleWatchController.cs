using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using marketperry.Models;
using Microsoft.AspNetCore.Authorization;

namespace marketperry.Areas.Admin.Controllers
{
   
    [Authorize(Roles = "Admin")]
    public class AppleWatchController : Controller
    {
        private readonly applicationDbContext _context;

        public AppleWatchController(applicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AppleWatch
        // public IActionResult Index()
        // {
        //     return View();
        // }
        public async Task<IActionResult> Index()
        {
              return _context.applewatchs != null ? 
                          View(await _context.applewatchs.ToListAsync()) :
                          Problem("Entity set 'applicationDbContext.applewatchs'  is null.");
        }

        // GET: Admin/AppleWatch/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.applewatchs == null)
            {
                return NotFound();
            }

            var appleWatch = await _context.applewatchs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appleWatch == null)
            {
                return NotFound();
            }

            return View(appleWatch);
        }

        // GET: Admin/AppleWatch/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AppleWatch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Create( AppleWatch appleWatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appleWatch);
                await _context.SaveChangesAsync();
                return Redirect("/Admin/AppleWatch");
            }
            return View(appleWatch);
        }

        // GET: Admin/AppleWatch/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.applewatchs == null)
            {
                return NotFound();
            }

            var appleWatch = await _context.applewatchs.FindAsync(id);
            if (appleWatch == null)
            {
                return NotFound();
            }
            return View(appleWatch);
        }

        // POST: Admin/AppleWatch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WtachName,Status,Price,Thumbnail,Config")] AppleWatch appleWatch)
        {
            if (id != appleWatch.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appleWatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppleWatchExists(appleWatch.Id))
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
            return View(appleWatch);
        }

        // GET: Admin/AppleWatch/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.applewatchs == null)
            {
                return NotFound();
            }

            var appleWatch = await _context.applewatchs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appleWatch == null)
            {
                return NotFound();
            }

            return View(appleWatch);
        }

        // POST: Admin/AppleWatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.applewatchs == null)
            {
                return Problem("Entity set 'applicationDbContext.applewatchs'  is null.");
            }
            var appleWatch = await _context.applewatchs.FindAsync(id);
            if (appleWatch != null)
            {
                _context.applewatchs.Remove(appleWatch);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppleWatchExists(int id)
        {
          return (_context.applewatchs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
