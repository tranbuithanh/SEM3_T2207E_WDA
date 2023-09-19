using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sem3.StudentManage;
using Sem3.StudentManage.Models;

namespace Sem3.StudentManage.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContect _context;

        public StudentController(StudentDbContect context)
        {
            _context = context;
        }

        // GET: Student
        public async Task<IActionResult> Index()
        {
              return _context.StudentModel != null ? 
                          View(await _context.StudentModel.ToListAsync()) :
                          Problem("Entity set 'StudentDbContect.StudentModel'  is null.");
        }

        // GET: Student/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.StudentModel == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // GET: Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,StudentName,BirthDay,Address,Image")] StudentModel studentModel, IFormFile image)
        {
            foreach (var key in ModelState.Keys)
            {
                if (ModelState[key].Errors.Any())
                {
                    var errorMessage = ModelState[key].Errors.First().ErrorMessage;
                    // In ra tên trường và thông báo lỗi
                    Console.WriteLine($"Field: {key}, Error: {errorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        image.CopyTo(memoryStream);
                        var imgBytes = memoryStream.ToArray();
                        var base64String = Convert.ToBase64String(imgBytes);
                        studentModel.Image = base64String;
                    }
                }
                _context.Add(studentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentModel);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.StudentModel == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel.FindAsync(id);
            if (studentModel == null)
            {
                return NotFound();
            }
            return View(studentModel);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,StudentName,BirthDay,Address,Image")] StudentModel studentModel)
        {
            if (id != studentModel.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentModelExists(studentModel.StudentId))
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
            return View(studentModel);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.StudentModel == null)
            {
                return NotFound();
            }

            var studentModel = await _context.StudentModel
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (studentModel == null)
            {
                return NotFound();
            }

            return View(studentModel);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.StudentModel == null)
            {
                return Problem("Entity set 'StudentDbContect.StudentModel'  is null.");
            }
            var studentModel = await _context.StudentModel.FindAsync(id);
            if (studentModel != null)
            {
                _context.StudentModel.Remove(studentModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentModelExists(string id)
        {
          return (_context.StudentModel?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
