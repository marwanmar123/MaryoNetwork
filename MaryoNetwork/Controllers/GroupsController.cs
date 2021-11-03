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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _db.Groups.Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(string id)
        {
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

    }
}
