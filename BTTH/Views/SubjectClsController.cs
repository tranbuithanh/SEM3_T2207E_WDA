using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BTTH.Data;
using BTTH.Models;

namespace BTTH.Views
{
    public class SubjectClsController : Controller
    {
        private readonly BTTHMVCContext _context;

        public SubjectClsController(BTTHMVCContext context)
        {
            _context = context;
        }

        // GET: SubjectCls
        public async Task<IActionResult> Index()
        {
              return _context.SubjectCls != null ? 
                          View(await _context.SubjectCls.ToListAsync()) :
                          Problem("Entity set 'BTTHMVCContext.SubjectCls'  is null.");
        }

        // GET: SubjectCls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SubjectCls == null)
            {
                return NotFound();
            }

            var subjectCls = await _context.SubjectCls
                .FirstOrDefaultAsync(m => m.Sbjid == id);
            if (subjectCls == null)
            {
                return NotFound();
            }

            return View(subjectCls);
        }

        // GET: SubjectCls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubjectCls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Sbjid,SbjName,SbjDescription,SbjOrder")] SubjectCls subjectCls)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subjectCls);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subjectCls);
        }

        // GET: SubjectCls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SubjectCls == null)
            {
                return NotFound();
            }

            var subjectCls = await _context.SubjectCls.FindAsync(id);
            if (subjectCls == null)
            {
                return NotFound();
            }
            return View(subjectCls);
        }

        // POST: SubjectCls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Sbjid,SbjName,SbjDescription,SbjOrder")] SubjectCls subjectCls)
        {
            if (id != subjectCls.Sbjid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subjectCls);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubjectClsExists(subjectCls.Sbjid))
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
            return View(subjectCls);
        }

        // GET: SubjectCls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SubjectCls == null)
            {
                return NotFound();
            }

            var subjectCls = await _context.SubjectCls
                .FirstOrDefaultAsync(m => m.Sbjid == id);
            if (subjectCls == null)
            {
                return NotFound();
            }

            return View(subjectCls);
        }

        // POST: SubjectCls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SubjectCls == null)
            {
                return Problem("Entity set 'BTTHMVCContext.SubjectCls'  is null.");
            }
            var subjectCls = await _context.SubjectCls.FindAsync(id);
            if (subjectCls != null)
            {
                _context.SubjectCls.Remove(subjectCls);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectClsExists(int id)
        {
          return (_context.SubjectCls?.Any(e => e.Sbjid == id)).GetValueOrDefault();
        }
    }
}
