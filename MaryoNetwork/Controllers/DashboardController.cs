using MaryoNetwork.Data;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPostService _postService;

        public DashboardController(ApplicationDbContext db, IPostService postService)
        {
            _db = db;
            _postService = postService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(_db.Categories.ToList());
        }
        public IActionResult PostCatDashboard(string id)
        {
            Category ctgr = _db.Categories.Find(id);
            var list = _db.Posts
                .Include(c => c.Comments
                .Where(c => c.PostId == c.Post.Id))
                .Include(u => u.User)
                .Where(x => x.CategoryId == id)
                .OrderByDescending(y => y.CreatedOn)
                .ToList();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approve(bool approved, string id)
        {

            var resId = _db.Posts.FirstOrDefault(a => a.Id == id);
            resId.Approved = approved;
            _db.SaveChanges();
            return RedirectToAction("PostCatDashboard", "Dashboard", new {id= resId.CategoryId});

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult deletePost(string id)
        {
            
            var resId = _db.Posts.Include(a=>a.Comments).FirstOrDefault(a=>a.Id== id);
            foreach(var a in resId.Comments)
            {
                _db.Remove(a);
            }
            _db.Remove(resId);
            _db.SaveChanges();
            return RedirectToAction("PostCatDashboard", "Dashboard", new { id = resId.CategoryId });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> deleteComment(string id, Post post)
        {
            var resId = _db.Comments.FirstOrDefault(a => a.Id == id);
            _db.Remove(resId);
            await _db.SaveChangesAsync();
            return RedirectToAction("PostCatDashboard", "Dashboard", new {id = post.CategoryId});

        }
    }
}
