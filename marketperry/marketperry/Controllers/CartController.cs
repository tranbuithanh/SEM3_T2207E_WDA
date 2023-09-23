using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using marketperry.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace marketperry.Controllers
{
    public class CartController : Controller
    {
        private readonly applicationDbContext _context;

        public CartController(applicationDbContext context)
        {
            _context = context;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var email = User.Identity.Name;
            var userLogin = _context.accounts.FirstOrDefault(u => u.Email == email);
            
              return _context.carts != null ? 
                          View( await _context.carts.Where(c => c.UserId == userLogin.Id).ToListAsync()) :
                          Problem("Entity set 'applicationDbContext.carts'  is null.");
        }

        // GET: Cart/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // GET: Cart/Create
      

        // POST: Cart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
       
        public async Task<IActionResult> Create(int id,[Bind("Quantity,Price")] Cart cart)
        {
            var email = User.Identity.Name;
            var userLogin = _context.accounts.FirstOrDefault(u => u.Email == email);
            cart.UserId = userLogin.Id;
            cart.Price = cart.Price * cart.Quantity;
            _context.Add(cart);
            await _context.SaveChangesAsync();
            return Redirect("/cart");
        }

        // GET: Cart/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // POST: Cart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PhoneId,UserId,Quantity,Price")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
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
            return Ok(cart);
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.carts == null)
            {
                return NotFound();
            }

            var cart = await _context.carts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return Ok(cart);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.carts == null)
            {
                return Problem("Entity set 'applicationDbContext.carts'  is null.");
            }
            var cart = await _context.carts.FindAsync(id);
            if (cart != null)
            {
                _context.carts.Remove(cart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
          return (_context.carts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
