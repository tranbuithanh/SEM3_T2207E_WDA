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

 
    public class StudentsController : Controller
    {
        private readonly BTTHMVCContext _context;

        public StudentsController(BTTHMVCContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
              return _context.Student != null ? 
                          View(await _context.Student.ToListAsync()) :
                          Problem("Entity set 'BTTHMVCContext.Student'  is null.");
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Stdid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName");
            return View();
            
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Stdid,StdName,StdBirth,StdTel,StdAdr,StdImg,Clsid")] Student student)
        {
           // if (ModelState.IsValid)
            //{
                _context.Add(student);
                await _context.SaveChangesAsync();
             //   return RedirectToAction(nameof(Index));
            //}
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName", student.Clsid);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName", student.Clsid);

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Stdid,StdName,StdBirth,StdTel,StdAdr,StdImg,Clsid")] Student student)
        {
            if (id != student.Stdid)
            {
                return NotFound();
            }

//     if (ModelState.IsValid)
  //  {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Stdid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            //    return RedirectToAction(nameof(Index));
          //  }
            ViewData["Clsid"] = new SelectList(_context.ClassCourse, "Clsid", "ClsName", student.Clsid);

            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Student == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Stdid == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Student == null)
            {
                return Problem("Entity set 'BTTHMVCContext.Student'  is null.");
            }
            var student = await _context.Student.FindAsync(id);
            if (student != null)
            {
                _context.Student.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Student?.Any(e => e.Stdid == id)).GetValueOrDefault();
        }
    }
}
