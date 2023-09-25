using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMVCforMoviePage.DbContextFolder;
using WebMVCforMoviePage.Models;

namespace WebMVCforMoviePage.Controllers
{
    public class MovieModelsController : Controller
    {
        private readonly UserDbContext _context;

        public MovieModelsController(UserDbContext context)
        {
            _context = context;
        }

        // GET: MovieModels
        public async Task<IActionResult> Index()
        {
            var userDbContext = _context.MovieModel.Include(m => m.Category);
            return View(await userDbContext.ToListAsync());
        }

        // GET: MovieModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MovieModel == null)
            {
                return NotFound();
            }

            var movieModel = await _context.MovieModel
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // GET: MovieModels/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "CategoryID", "CategoryID");
            return View();
        }

        // POST: MovieModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovieId,CategoryId,Title,Description,Author,Genre,ReleaseDate,CoverImgUrl,Duration")] MovieModel movieModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "CategoryID", "CategoryID", movieModel.CategoryId);
            return View(movieModel);
        }

        // GET: MovieModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MovieModel == null)
            {
                return NotFound();
            }

            var movieModel = await _context.MovieModel.FindAsync(id);
            if (movieModel == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "CategoryID", "CategoryID", movieModel.CategoryId);
            return View(movieModel);
        }

        // POST: MovieModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,CategoryId,Title,Description,Author,Genre,ReleaseDate,CoverImgUrl,Duration")] MovieModel movieModel)
        {
            if (id != movieModel.MovieId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieModelExists(movieModel.MovieId))
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
            ViewData["CategoryId"] = new SelectList(_context.CategoryModel, "CategoryID", "CategoryID", movieModel.CategoryId);
            return View(movieModel);
        }

        // GET: MovieModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MovieModel == null)
            {
                return NotFound();
            }

            var movieModel = await _context.MovieModel
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.MovieId == id);
            if (movieModel == null)
            {
                return NotFound();
            }

            return View(movieModel);
        }

        // POST: MovieModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MovieModel == null)
            {
                return Problem("Entity set 'UserDbContext.MovieModel'  is null.");
            }
            var movieModel = await _context.MovieModel.FindAsync(id);
            if (movieModel != null)
            {
                _context.MovieModel.Remove(movieModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieModelExists(int id)
        {
          return (_context.MovieModel?.Any(e => e.MovieId == id)).GetValueOrDefault();
        }
    }
}
