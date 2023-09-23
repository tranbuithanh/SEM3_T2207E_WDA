using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using marketperry.Models;

namespace marketperry.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class mobileController : Controller
    {
        private readonly applicationDbContext _context;

        public mobileController(applicationDbContext context)
        {
            _context = context;
        }

        // GET: mobile
        public async Task<IActionResult> Index()

        {
            
              return _context.mobilephones != null ? 
                          View(await _context.mobilephones.ToListAsync()) :
                          Problem("Entity set 'applicationDbContext.mobilephones'  is null.");
        }

        // GET: mobile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.mobilephones == null)
            {
                return NotFound();
            }

            var mobilephone = await _context.mobilephones
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mobilephone == null)
            {
                return NotFound();
            }

            return View(mobilephone);
        }

        // GET: mobile/Create
       
        public IActionResult Create()
        {

            return View();
        }

        // POST: mobile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( mobilephone mobilephone)
        {

            
            if (ModelState.IsValid)
            {
                _context.Add(mobilephone);
                await _context.SaveChangesAsync();
                return Redirect("/admin/mobile");
            }

            using (var memoryStream = new MemoryStream())
            {
                await mobilephone.imageBase.CopyToAsync(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                mobilephone.Thumbnail = "data:image/webp;base64," + Convert.ToBase64String(bytes);
               _context.mobilephones.Add(mobilephone);
               await _context.SaveChangesAsync();
                
            }
            return Redirect("/Admin/mobile");
        }

        
        // GET: mobile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.mobilephones == null)
            {
                return NotFound();
            }

            var mobilephone = await _context.mobilephones.FindAsync(id);
            if (mobilephone == null)
            {
                return NotFound();
            }
            return View(mobilephone);
        }

        // POST: mobile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public async Task<IActionResult> Edit(int id, mobilephone mobilephone)
        {
            if (id != mobilephone.Id)
            {
                return NotFound();
            }
            

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mobilephone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mobilephoneExists(mobilephone.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Redirect("/Admin/mobile");
            }
            return View(mobilephone);
        }

        // GET: mobile/Delete/5
      

        // POST: mobile/Delete/5
        
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.mobilephones == null)
            {
                return Problem("Entity set 'applicationDbContext.mobilephones'  is null.");
            }
            var mobilephone = await _context.mobilephones.FindAsync(id);
            if (mobilephone != null)
            {
                _context.mobilephones.Remove(mobilephone);
            }
            
            await _context.SaveChangesAsync();
            return Redirect("/Admin/mobile");
        }

        private bool mobilephoneExists(int id)
        {
          return (_context.mobilephones?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
