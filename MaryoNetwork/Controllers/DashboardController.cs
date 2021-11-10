using MaryoNetwork.Data;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardController(
            ApplicationDbContext db,
            IPostService postService,
            IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _postService = postService;
            _httpContextAccessor = httpContextAccessor;
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
                .Include(u=> u.Images)
                .Include(c => c.Comments)
                .Include(c => c.Likes)
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
        public ActionResult DeletePost(string id)
        {
            
            var resId = _db.Posts.Include(a=>a.Comments).Include(a=>a.Likes).Include(a=>a.Images).FirstOrDefault(a=>a.Id== id);
            foreach(var a in resId.Comments)
            {
                _db.Remove(a);
            }
            foreach (var l in resId.Likes)
            {
                _db.Remove(l);
            }
            foreach (var m in resId.Images)
            {
                _db.Remove(m);
            }
            _db.Remove(resId);
            _db.SaveChanges();
            return RedirectToAction("PostCatDashboard", "Dashboard", new { id = resId.CategoryId });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string postId, string content, Comment comment)
        {
            var post = await _postService.GetPostWithUserAsync(postId);



            var addComment = new Comment
            {
                Content = content,
                UserId = comment.UserId,
                PostId = post.Id
            };
            await _db.AddAsync(addComment);
            await _db.SaveChangesAsync();

            return RedirectToAction("PostCatDashboard", "Dashboard", new { id = post.CategoryId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteComment(string id, string postId)
        {
            var post = await _postService.GetPostWithUserAsync(postId);
            var resId = _db.Comments.Include(a=>a.Post).FirstOrDefault(a => a.Id == id);
            _db.Remove(resId);
            await _db.SaveChangesAsync();
            return RedirectToAction("PostCatDashboard", "Dashboard", new { id = resId.Post.CategoryId });

        }


        public IActionResult Editor()
        {
            var components = _db.Editors.Include(e => e.User).ToList();
            return View(components);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> deleteComponentEditor(string id, Post post)
        {
            var resId = _db.Editors.FirstOrDefault(a => a.Id == id);
            _db.Remove(resId);
            await _db.SaveChangesAsync();
            return RedirectToAction("Editor", "Dashboard", new { id = post.CategoryId });

        }

        public async Task<IActionResult> groups()
        {
            var applicationDbContext = _db.Groups.Include(a => a.Members).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteGroup(string id)
        {

            var resId = _db.Groups.Include(a => a.Members).FirstOrDefault(a => a.Id == id);
            foreach (var a in resId.Members)
            {
                _db.Remove(a);
            }
            _db.Remove(resId);
           await _db.SaveChangesAsync();
            return RedirectToAction("Groups", "Dashboard");

        }
    }
}
