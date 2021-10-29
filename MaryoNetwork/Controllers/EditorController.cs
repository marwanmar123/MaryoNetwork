using MaryoNetwork.Data;
using MaryoNetwork.Models.Editors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    public class EditorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EditorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var components = _db.Editors.ToList();
            return View(components);
        }
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _db.Editors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

         public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Editor editor)
        {
            var addEditor = new Editor
            {
                Title = editor.Title,
                Html = editor.Html,
                Css = editor.Css,
                Js = editor.Js,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };
            await _db.AddAsync(addEditor);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
