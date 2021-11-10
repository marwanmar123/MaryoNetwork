using MaryoNetwork.Data;
using MaryoNetwork.Models;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public IActionResult Index()
        {
            var homeData = new HomeViewModel()
            {
                Category = _db.Categories.ToList(),
                Post = _db.Posts.Include(a => a.User).Include(a => a.Comments).Include(a => a.Likes).Include(a => a.Images).ToList()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
