using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MaryoNetwork.Data;
using MaryoNetwork.Models.Groups;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace MaryoNetwork.Controllers
{
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _db;

        public GroupsController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string search = null)
        {
            IEnumerable<Group> groups;
            if (!string.IsNullOrEmpty(search))
            {
                groups = _db.Groups.Include(e => e.Members).Include(a => a.User).Where(s => s.Name.ToLower().Contains(search.ToLower()));
            }
            else
            {
                groups = _db.Groups.Include(a => a.Members).Include(a => a.User);
            }
                //var applicationDbContext = _db.Groups.Include(a=>a.Members).Include(a => a.User);
            return View(groups);
        }
        public async Task<IActionResult> Details(string id)
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isFollower = _db.GroupMembers.SingleOrDefault(a => a.UserId == currentUser && a.GroupId == id);
            ViewBag.isfollow = isFollower;
            if (id == null)
            {
                return NotFound();
            }

            var group = await _db.Groups
                .Include(a => a.User)
                .Include(a=>a.Members)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (group == null)
            {
                return NotFound();
            }

            return View(group);
        }
        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Group group)
        {
            var createGroup = new Group
            {
                Name = group.Name,
                Description = group.Description,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    createGroup.Image = dataStream.ToArray();
                }
            }

            await _db.AddAsync(createGroup);
            await _db.SaveChangesAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Follow(GroupMembers member)
        {
            var addFollow = new GroupMembers
            {
                GroupId = member.GroupId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            await _db.AddAsync(addFollow);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Quitter(string id)
        {
            var favId = _db.GroupMembers.Include(a=>a.Group).FirstOrDefault(a => a.Id == id);
            _db.Remove(favId);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
