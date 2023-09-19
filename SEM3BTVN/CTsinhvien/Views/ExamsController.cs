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
    public class ExamsController : Controller
    {
        private readonly Sem3MvcContext _context;

        public ExamsController(Sem3MvcContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            var sem3MvcContext = _context.Exams.Include(e => e.Clsroom).Include(e => e.Student).Include(e => e.Subjectcls);
            return View(await sem3MvcContext.ToListAsync());
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Clsroom)
                .Include(e => e.Student)
                .Include(e => e.Subjectcls)
                .FirstOrDefaultAsync(m => m.Exid == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            ViewData["Clsroomid"] = new SelectList(_context.Classrooms, "Clsroomid", "Clsroomid");
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid");
            ViewData["Subjectclsid"] = new SelectList(_context.Subjectclas, "Subjectclsid", "Subjectclsid");
            return View();
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Exid,Exresult,Exdate,Excourse,Extime,Studentid,Subjectclsid,Clsroomid")] Exam exam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Clsroomid"] = new SelectList(_context.Classrooms, "Clsroomid", "Clsroomid", exam.Clsroomid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", exam.Studentid);
            ViewData["Subjectclsid"] = new SelectList(_context.Subjectclas, "Subjectclsid", "Subjectclsid", exam.Subjectclsid);
            return View(exam);
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["Clsroomid"] = new SelectList(_context.Classrooms, "Clsroomid", "Clsroomid", exam.Clsroomid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", exam.Studentid);
            ViewData["Subjectclsid"] = new SelectList(_context.Subjectclas, "Subjectclsid", "Subjectclsid", exam.Subjectclsid);
            return View(exam);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Exid,Exresult,Exdate,Excourse,Extime,Studentid,Subjectclsid,Clsroomid")] Exam exam)
        {
            if (id != exam.Exid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.Exid))
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
            ViewData["Clsroomid"] = new SelectList(_context.Classrooms, "Clsroomid", "Clsroomid", exam.Clsroomid);
            ViewData["Studentid"] = new SelectList(_context.Students, "Studentid", "Studentid", exam.Studentid);
            ViewData["Subjectclsid"] = new SelectList(_context.Subjectclas, "Subjectclsid", "Subjectclsid", exam.Subjectclsid);
            return View(exam);
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Exams == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Clsroom)
                .Include(e => e.Student)
                .Include(e => e.Subjectcls)
                .FirstOrDefaultAsync(m => m.Exid == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Exams == null)
            {
                return Problem("Entity set 'Sem3MvcContext.Exams'  is null.");
            }
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
          return (_context.Exams?.Any(e => e.Exid == id)).GetValueOrDefault();
        }
    }
}
