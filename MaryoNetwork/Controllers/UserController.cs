using MaryoNetwork.Data;
using MaryoNetwork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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


            return View(_db.Users.ToList());
        }

        public async Task<IActionResult> Profile(string id)
        {
             //ProfileViewModel db = new ProfileViewModel();
            if (id == null)
            {
                return NotFound();
            }

            //var data = await _db.Posts.Include(c => c.Comments).Include(u => u.User).Where(x => x.UserId == id).Include(x => x.Category).OrderByDescending(y => y.CreatedOn).ToListAsync();

            var data = new ProfileViewModel
            {
                User = await _db.Users.FirstOrDefaultAsync(u => u.Id == id),
                Post = await _db.Posts.Include(c => c.Comments).Include(u => u.User).Where(x => x.UserId == id).Include(x => x.Category).OrderByDescending(y => y.CreatedOn).ToListAsync()
                //Comment = _db.Comments.ToList(),
                //Like = _db.Likes.ToList()
            };

            return View(data);
        }
    }
}
