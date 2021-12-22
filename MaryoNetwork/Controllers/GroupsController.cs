using MaryoNetwork.Data;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Groups;
using MaryoNetwork.Models.Images;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Services.Posts;
using MaryoNetwork.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public GroupsController(
            ApplicationDbContext db,
            IPostService postService,
            IUserService userService)
        {
            _db = db;
            _postService = postService;
            _userService = userService;
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
                .Include(a => a.Members)
                .ThenInclude(a => a.User)
                .Include(a => a.Post)
                    .ThenInclude(a => a.Images)
                .Include(a => a.Post)
                    .ThenInclude(a => a.Comments)
                    .ThenInclude(a => a.User)
                .Include(a => a.Post)
                    .ThenInclude(a => a.Likes)
                    .ThenInclude(a => a.User)
                .Include(a => a.Post)
                    .ThenInclude(a => a.User)
                .Include(a => a.Post)
                    .ThenInclude(a => a.FavoritePost)
                //.Include(a => a.Post)
                //   .ThenInclude(a => a.FavoritePost.)
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
            return RedirectToAction("index");
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
            var favId = _db.GroupMembers.Include(a => a.Group).FirstOrDefault(a => a.Id == id);
            _db.Remove(favId);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroupPost(Post post, List<IFormFile> files, string postId, string groupId)
        {
            var group = await _db.Groups.FirstOrDefaultAsync(m => m.Id == groupId);
            var postt = await _postService.GetPostWithUserAsync(postId);
            var totalLike = _db.Likes.Where(l => l.PostId == postId);

            var addPost = new Post
            {
                Content = post.Content,
                UserId = await _userService.GetCurrentUserIdAsync(),
                GroupId = post.GroupId,
                Approved = false
            };

            _db.Add(addPost);

            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var extension = Path.GetExtension(file.FileName);
                var fileModel = new Image
                {
                    CreatedOn = DateTime.UtcNow,
                    FileType = file.ContentType,
                    Extension = extension,
                    Name = fileName,
                    UploadedById = await _userService.GetCurrentUserIdAsync(),
                    PostId = addPost.Id
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    fileModel.Data = dataStream.ToArray();
                }
                await _db.AddAsync(fileModel);
                await _db.SaveChangesAsync();
            }

            await _db.SaveChangesAsync();

            return RedirectToAction("Details", "Groups", new { id = groupId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateGroupComment(string postId, string content, Comment comment)
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

            return RedirectToAction("Details", "Groups", new { id = post.GroupId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LikeGroupPost(string postId)
        {
            var post = await _postService.GetPostWithUserAsync(postId);
            var userId = await _userService.GetCurrentUserIdAsync();

            var like = _db.Likes.FirstOrDefault(l => l.PostId == postId && l.UserId == userId);


            if (like == null)
            {
                like = new Like
                {
                    UserId = userId,
                    PostId = post.Id
                };
                await _db.AddAsync(like);
            }
            else
            {
                _db.Likes.Remove(like);
            }
            await _db.SaveChangesAsync();
            var likeCount = _db.Likes.Where(l => l.PostId == postId).CountAsync();
            ViewBag.likeCount = likeCount;

            return RedirectToAction("Details", "Groups", new { id = post.GroupId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditGroupName(string groupId, string nameGroup)
        {
            var group = await _db.Groups.FirstOrDefaultAsync(m => m.Id == groupId);
            group.Name = nameGroup;
            _db.SaveChanges();

            return RedirectToAction("Details", "Groups", new { id = groupId });
        }

    }
}
