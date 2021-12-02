using MaryoNetwork.Data;
using MaryoNetwork.Models.Editors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    [Authorize]
    public class EditorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EditorController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index(string search = null)
        {
            IEnumerable<Editor> editors;
            if (!string.IsNullOrEmpty(search))
            {
                editors = _db.Editors.Include(e => e.User).Where(s => s.Title.ToLower().Contains(search.ToLower()));
            }
            else
            {
                editors = _db.Editors.Include(e => e.User).ToList();
            }
            //var components = _db.Editors.Include(e=>e.User).ToList();
            return View(editors);
        }
        public async Task<IActionResult> Details(string id, Favorite favorite)
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isExist = _db.Favorites.SingleOrDefault(a => a.UserId == currentUser && a.EditorId == id);
            ViewBag.exist = isExist;
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

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    addEditor.Image = dataStream.ToArray();
                }
                //await _userManager.UpdateAsync(user);
            }

            await _db.AddAsync(addEditor);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Favorites(string editorId)
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isExist = _db.Favorites.SingleOrDefault(a => a.UserId == currentUser && a.EditorId == editorId);
            if (isExist == null)
            {
                var addFavorite = new Favorite
                {
                    UserId = currentUser,
                    EditorId = editorId
                };
                await _db.AddAsync(addFavorite);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> MyFavorites()
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorit = await _db.Favorites
                .Where(a => a.UserId == currentUser)
                .Include(a => a.Editor.User)
                .Include(a => a.Editor)
                .ToListAsync();
            return View(favorit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFavorite(string id, Favorite favorite)
        {
            var favId = _db.Favorites.Include(a => a.Editor).FirstOrDefault(a => a.Id == id);
            _db.Remove(favId);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(MyFavorites));

        }
    }
}
