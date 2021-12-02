using MaryoNetwork.Data;
using MaryoNetwork.Models;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Models.Requests;
using MaryoNetwork.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        //[Authorize]
        public IActionResult Index(string search = null)
        {
            IEnumerable<Post> posts;
            if (!string.IsNullOrEmpty(search))
            {
                posts = _db.Posts
                .Include(u => u.User)
                .Include(i => i.Images)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .Include(u => u.Category)
                .Include(u => u.FavoritePost)
                .ThenInclude(u => u.User)
                .Where(s => s.Content.ToLower().Contains(search.ToLower()) && s.Approved == true)
                .ToList();

            }
            else
            {
                posts = _db.Posts
                .Include(i => i.Images)
                .Include(c => c.Comments)
                .Include(l => l.Likes)
                .Include(u => u.User)
                .Include(u => u.Category)
                .Include(u => u.FavoritePost)
                .ThenInclude(u => u.User)
                .OrderByDescending(y => y.CreatedOn)
                .ToList();
            }

            var homeData = new HomeViewModel()
            {
                Category = _db.Categories.ToList(),
                Post = (List<Post>)posts,
                Group = _db.Groups.Include(e => e.Members).ToList(),
                Request = _db.Requests.ToList()
            };
            return View(homeData);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        //[Authorize]
        public IActionResult Posts(string id)
        {

            Category ctgr = _db.Categories.Find(id);
            var result = _db.Posts.Where(x => x.CategoryId == id).OrderByDescending(y => y.CreatedOn).ToList();
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(Request request)
        {
            var addRequest = new Request()
            {
                Content = request.Content,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            _db.Add(addRequest);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult voteIncremment(string id, int add)
        {

            var reqId = _db.Requests.FirstOrDefault(a => a.Id == id);
            if (reqId.Like < 10)
            {
                reqId.Like += add;
            }
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRequest(string id)
        {

            var reqId = _db.Requests.FirstOrDefault(a => a.Id == id);
            _db.Remove(reqId);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }









        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
