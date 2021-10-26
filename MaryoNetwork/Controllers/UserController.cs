using MaryoNetwork.Data;
using MaryoNetwork.Models;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }


        public IActionResult Index()
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_db.Users.Where(a=>a.Id != currentUser).ToList());
        }

        public async Task<IActionResult> Profile(User user, string id)
        {
            if (user == null)
            {
                return NotFound();
            }
            var data = new ProfileViewModel
            {
                Users = await _db.Users.Include(p => p.Posts.Where(p => p.UserId == user.Id)).ThenInclude(p => p.Comments).Include(a => a.Posts).ThenInclude(x => x.Category).ToListAsync(),
                User = _db.Users.FirstOrDefault(a => a.Id == id)
            };

            return View(data);
        }

        
        private bool UserExists(string id)
        {
            return _db.Users.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: AspNetUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var aspNetUser = await _db.Users.FindAsync(id);
            _db.Users.Remove(aspNetUser);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}