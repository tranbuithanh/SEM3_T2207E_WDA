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
    public class ClassCoursesController : Controller
    {
        private readonly BTTHMVCContext _context;

        public ClassCoursesController(BTTHMVCContext context)
        {
            _context = context;
        }

        // GET: ClassCourses
        public async Task<IActionResult> Index()
        {
              return _context.ClassCourse != null ? 
                          View(await _context.ClassCourse.ToListAsync()) :
                          Problem("Entity set 'BTTHMVCContext.ClassCourse'  is null.");
        }

        // GET: ClassCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ClassCourse == null)
            {
                return NotFound();
            }

            var classCourse = await _context.ClassCourse
                .FirstOrDefaultAsync(m => m.Clsid == id);
            if (classCourse == null)
            {
                return NotFound();
            }

            return View(classCourse);
        }

        // GET: ClassCourses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClassCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Clsid,ClsName,ClsDescription,ClsOrder")] ClassCourse classCourse)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classCourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classCourse);
        }

        // GET: ClassCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ClassCourse == null)
            {
                return NotFound();
            }

            var classCourse = await _context.ClassCourse.FindAsync(id);
            if (classCourse == null)
            {
                return NotFound();
            }
            return View(classCourse);
        }

        // POST: ClassCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Clsid,ClsName,ClsDescription,ClsOrder")] ClassCourse classCourse)
        {
            if (id != classCourse.Clsid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassCourseExists(classCourse.Clsid))
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
            return View(classCourse);
        }

        // GET: ClassCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ClassCourse == null)
            {
                return NotFound();
            }

            var classCourse = await _context.ClassCourse
                .FirstOrDefaultAsync(m => m.Clsid == id);
            if (classCourse == null)
            {
                return NotFound();
            }

            return View(classCourse);
        }

        // POST: ClassCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ClassCourse == null)
            {
                return Problem("Entity set 'BTTHMVCContext.ClassCourse'  is null.");
            }
            var classCourse = await _context.ClassCourse.FindAsync(id);
            if (classCourse != null)
            {
                _context.ClassCourse.Remove(classCourse);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassCourseExists(int id)
        {
          return (_context.ClassCourse?.Any(e => e.Clsid == id)).GetValueOrDefault();
        }
    }
}
