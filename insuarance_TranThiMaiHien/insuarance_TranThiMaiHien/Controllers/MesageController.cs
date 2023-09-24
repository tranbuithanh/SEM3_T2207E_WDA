using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using insuarance_TranThiMaiHien.Entities;
using insuarance_TranThiMaiHien.Models;

namespace insuarance_TranThiMaiHien.Controllers
{
    public class MesageController : Controller
    {
        private readonly DataContext _context;

        public MesageController(DataContext context)
        {
            _context = context;
        }

        // GET: Mesage
        public async Task<IActionResult> Index()
        {
              return _context.messages != null ? 
                          View(await _context.messages.ToListAsync()) :
                          Problem("Entity set 'DataContext.messages'  is null.");
        }

        // GET: Mesage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.messages == null)
            {
                return NotFound();
            }

            var message = await _context.messages
                .FirstOrDefaultAsync(m => m.id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // GET: Mesage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mesage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,fullname,address,phone,message,img_registration,status,created_at")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(message);
        }

        // GET: Mesage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.messages == null)
            {
                return NotFound();
            }

            var message = await _context.messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: Mesage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,fullname,address,phone,message,img_registration,status,created_at")] Message message)
        {
            if (id != message.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(message);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessageExists(message.id))
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
            return View(message);
        }

        // GET: Mesage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.messages == null)
            {
                return NotFound();
            }

            var message = await _context.messages
                .FirstOrDefaultAsync(m => m.id == id);
            if (message == null)
            {
                return NotFound();
            }

            return View(message);
        }

        // POST: Mesage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.messages == null)
            {
                return Problem("Entity set 'DataContext.messages'  is null.");
            }
            var message = await _context.messages.FindAsync(id);
            if (message != null)
            {
                _context.messages.Remove(message);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MessageExists(int id)
        {
          return (_context.messages?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
