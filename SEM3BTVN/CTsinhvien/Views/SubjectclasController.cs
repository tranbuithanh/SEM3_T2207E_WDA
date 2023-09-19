using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CTsinhvien.Models;

namespace CTsinhvien.Views
{
    public class SubjectclasController : Controller
    {
        private readonly Sem3MvcContext _context;

        public SubjectclasController(Sem3MvcContext context)
        {
            _context = context;
        }

        // GET: Subjectclas
        public async Task<IActionResult> Index()
        {
              return _context.Subjectclas != null ? 
                          View(await _context.Subjectclas.ToListAsync()) :
                          Problem("Entity set 'Sem3MvcContext.Subjectclas'  is null.");
        }

        // GET: Subjectclas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Subjectclas == null)
            {
                return NotFound();
            }

            var subjectcla = await _context.Subjectclas
                .FirstOrDefaultAsync(m => m.Subjectclsid == id);
            if (subjectcla == null)
            {
                return NotFound();
            }

            return View(subjectcla);
        }

        // GET: Subjectclas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjectclas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Subjectclsid,Subjectname,Course")] Subjectcla subjectcla)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectcla);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectcla);
        }

        // GET: Subjectclas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Subjectclas == null)
            {
                return NotFound();
            }

            var subjectcla = await _context.Subjectclas.FindAsync(id);
            if (subjectcla == null)
            {
                return NotFound();
            }
            return View(subjectcla);
        }

        // POST: Subjectclas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Subjectclsid,Subjectname,Course")] Subjectcla subjectcla)
        {
            if (id != subjectcla.Subjectclsid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectcla);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectclaExists(subjectcla.Subjectclsid))
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
            return View(subjectcla);
        }

        // GET: Subjectclas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Subjectclas == null)
            {
                return NotFound();
            }

            var subjectcla = await _context.Subjectclas
                .FirstOrDefaultAsync(m => m.Subjectclsid == id);
            if (subjectcla == null)
            {
                return NotFound();
            }

            return View(subjectcla);
        }

        // POST: Subjectclas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Subjectclas == null)
            {
                return Problem("Entity set 'Sem3MvcContext.Subjectclas'  is null.");
            }
            var subjectcla = await _context.Subjectclas.FindAsync(id);
            if (subjectcla != null)
            {
                _context.Subjectclas.Remove(subjectcla);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectclaExists(int id)
        {
          return (_context.Subjectclas?.Any(e => e.Subjectclsid == id)).GetValueOrDefault();
        }
    }
}
