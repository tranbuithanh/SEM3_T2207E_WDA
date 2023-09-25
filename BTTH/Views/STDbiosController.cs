using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTTH.Data;
using BTTH.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace BTTH.Views
{
    [Authorize(Roles = "Administrator")]
  
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Teacher")]
    public class STDbiosController : Controller
    {
        private readonly BTTHMVCContext _context;

        public STDbiosController(BTTHMVCContext context)
        {
            _context = context;
        }

        // GET: STDbios
        public async Task<IActionResult> Index()
        {
              return _context.STDbio != null ? 
                          View(await _context.STDbio.ToListAsync()) :
                          Problem("Entity set 'BTTHMVCContext.STDbio'  is null.");
        }

        // GET: STDbios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.STDbio == null)
            {
                return NotFound();
            }

            var sTDbio = await _context.STDbio
                .FirstOrDefaultAsync(m => m.STDbioId == id);
            if (sTDbio == null)
            {
                return NotFound();
            }

            return View(sTDbio);
        }

        // GET: STDbios/Create
        public IActionResult Create()
        {
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName" );
            ViewData["Stdid"] = new SelectList(_context.ClassCourse, "Stdid", "StdName" );

            return View();
        }

        // POST: STDbios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("STDbioId,Sbjid,Stdid,ExamMark,Progress,Status")] STDbio sTDbio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sTDbio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName", sTDbio.Sbjid);
            ViewData["Stdid"] = new SelectList(_context.ClassCourse, "Stdid", "StdName", sTDbio.Stdid);

            return View(sTDbio);
        }

        // GET: STDbios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.STDbio == null)
            {
                return NotFound();
            }

            var sTDbio = await _context.STDbio.FindAsync(id);
            if (sTDbio == null)
            {
                return NotFound();
            }
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName", sTDbio.Sbjid);
            ViewData["Stdid"] = new SelectList(_context.ClassCourse, "Stdid", "StdName", sTDbio.Stdid);

            return View(sTDbio);
        }

        // POST: STDbios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("STDbioId,Sbjid,Stdid,ExamMark,Progress,Status")] STDbio sTDbio)
        {
            if (id != sTDbio.STDbioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sTDbio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!STDbioExists(sTDbio.STDbioId))
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
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName", sTDbio.Sbjid);
            ViewData["Stdid"] = new SelectList(_context.ClassCourse, "Stdid", "StdName", sTDbio.Stdid);

            return View(sTDbio);
        }

        // GET: STDbios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.STDbio == null)
            {
                return NotFound();
            }

            var sTDbio = await _context.STDbio
                .FirstOrDefaultAsync(m => m.STDbioId == id);
            if (sTDbio == null)
            {
                return NotFound();
            }

            return View(sTDbio);
        }

        // POST: STDbios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.STDbio == null)
            {
                return Problem("Entity set 'BTTHMVCContext.STDbio'  is null.");
            }
            var sTDbio = await _context.STDbio.FindAsync(id);
            if (sTDbio != null)
            {
                _context.STDbio.Remove(sTDbio);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool STDbioExists(int id)
        {
          return (_context.STDbio?.Any(e => e.STDbioId == id)).GetValueOrDefault();
        }
    }
}
