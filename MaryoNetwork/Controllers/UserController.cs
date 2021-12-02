using MaryoNetwork.Data;
using MaryoNetwork.Models;
using MaryoNetwork.Models.Friends;
using MaryoNetwork.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;

        public UserController(ApplicationDbContext db, UserManager<User> userManager)
        {
            _db = db;
            _userManager = userManager;
        }


        public IActionResult Index(string search = null)
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<User> users;
            if (!string.IsNullOrEmpty(search))
            {
                users = _db.Users
                .Include(a => a.FriendRequestSent)
                .Include(a => a.FriendRequestReceived)
                .Where(s => s.FullName.ToLower().Contains(search.ToLower()) && s.Id != currentUser);
            }
            else
            {
                users = _db.Users
                .Where(a => a.Id != currentUser)
                .Include(a => a.FriendRequestSent)
                .Include(a => a.FriendRequestReceived).ToList();
            }
            //var usersList = _db.Users
            //    .Where(a => a.Id != currentUser )
            //    .Include(a=>a.FriendRequestSent)
            //    .Include(a=>a.FriendRequestReceived)
            //    .ToList();
            return View(users);
        }

        public async Task<IActionResult> Profile(User user, string id)
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (user == null)
            {
                return NotFound();
            }
            var data = new ProfileViewModel
            {
                Users = await _db.Users
                .Include(p => p.Images)
                .Include(p => p.Members)
                .Include(p => p.FriendRequestReceived)
                .Include(p => p.FriendRequestSent)
                .Include(p => p.Groups)
                .Include(p => p.Posts
                .Where(p => p.UserId == user.Id))
                .ThenInclude(p => p.Comments)
                .Include(a => a.Posts)
                .ThenInclude(x => x.Category)
                .Include(a => a.Posts)
                .ThenInclude(x => x.Likes)
                .Include(a => a.Posts)
                .ThenInclude(x => x.Images)
                .ToListAsync(),
                User = _db.Users.FirstOrDefault(a => a.Id == id)
            };
            var requestFriend = _db.Friends.SingleOrDefault(a => a.ReceiverId == currentUser && a.SenderId == id || a.SenderId == currentUser && a.ReceiverId == id);

            ViewBag.requestFriend = requestFriend;
            if (requestFriend != null)
            {
                ViewBag.waitFriend = requestFriend.RequestStatusId == "3";
                ViewBag.isFriend = requestFriend.RequestStatusId == "1";
            }
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
            return RedirectToAction("Index", "Role");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IsOnline(bool online, string id)
        {

            var resId = _db.Users.FirstOrDefault(a => a.Id == id);
            resId.IsOnline = online;
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");

        }


        public async Task<IActionResult> Addfriend(Friend friend)
        {

            var CurrentuserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var addFriend = new Friend()
            {
                SenderId = CurrentuserId,
                ReceiverId = friend.ReceiverId,
                RequestStatusId = "3"
            };
            _db.Add(addFriend);

            await _db.SaveChangesAsync();
            return RedirectToAction("profile", "User", new { id = friend.ReceiverId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveFriend(string RequestStatus, string id)
        {

            var resId = _db.Friends.FirstOrDefault(a => a.Id == id);
            resId.RequestStatusId = RequestStatus;
            if (resId.RequestStatusId == "2")
            {
                _db.Remove(resId);
            }
            _db.SaveChanges();
            return Redirect("/Identity/Account/Manage");
        }
    }
}