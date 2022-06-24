using MaryoNetwork.Data;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Comments;
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
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpContextFactory _httpContextFactory;

        public PostController(ApplicationDbContext db,
            IPostService postService,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IHttpContextFactory httpContextFactory)
        {
            _db = db;
            _postService = postService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _httpContextFactory = httpContextFactory;
        }

        [Route("Post/cat/{id?}")]
        [Authorize]
        public async Task<IActionResult> Index(string id, string search = null)
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Post> posts;
            if (!string.IsNullOrEmpty(search))
            {
                posts = await _db.Posts
               .Include(u => u.User)
               .Include(i => i.Images)
               .Include(c => c.Comments)
                   .ThenInclude(c => c.User)
               .Include(l => l.Likes)
                   .ThenInclude(l => l.User)
               .Include(u => u.Category)
               .Include(u => u.FavoritePost)
                   .ThenInclude(a => a.User)
               .Where(s => s.Content.ToLower().Contains(search.ToLower()) && s.Approved == true)
               .ToListAsync();

            }
            else
            {
                posts = await _db.Posts
                .Include(i => i.Images)
                .Include(c => c.Comments)
                    .ThenInclude(c => c.User)
                .Include(l => l.Likes)
                    .ThenInclude(l => l.User)
                .Include(u => u.User)
                .Include(u => u.Category)
                .Include(u => u.FavoritePost)
                    .ThenInclude(a => a.User)
                .Where(x => x.CategoryId == id && x.Approved == true)
                .OrderByDescending(y => y.CreatedOn)
                .ToListAsync();
            }

            Category ctgr = _db.Categories.Find(id);
            ViewBag.nameCat = ctgr.Name;
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(Post post, List<IFormFile> files, string postId)
        {
            var postt = await _postService.GetPostWithUserAsync(postId);
            var totalLike = _db.Likes.Where(l => l.PostId == postId);

            var addPost = new Post
            {
                Content = post.Content,
                UserId = await _userService.GetCurrentUserIdAsync(),
                CategoryId = post.CategoryId,
            };
            _ = User.IsInRole("admin") ? addPost.Approved = true : addPost.Approved = false;

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

            return RedirectToAction("index", "post", new { id = post.CategoryId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FavoritePost(string postId)
        {
            var post = await _postService.GetPostWithUserAsync(postId);
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isExist = _db.FavoritePosts.SingleOrDefault(a => a.UserId == currentUser && a.PostId == postId);
            if (isExist == null)
            {
                var addFavorite = new FavoritePost
                {
                    UserId = currentUser,
                    PostId = postId
                };
                await _db.AddAsync(addFavorite);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction("MyFavorites");
        }

        public async Task<IActionResult> MyFavorites()
        {
            var currentUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var favorit = await _db.FavoritePosts
                .Where(a => a.UserId == currentUser)
                .Include(a => a.Post)
                .ThenInclude(a => a.User)
                .Include(a => a.Post)
                .ThenInclude(a => a.Comments)
                .ThenInclude(a => a.User)
                .Include(a => a.Post)
                .ThenInclude(a => a.Likes)
                .ThenInclude(a => a.User)
                .Include(a => a.Post)
                .ThenInclude(a => a.Images)
                .Include(a => a.Post)
                .ThenInclude(a => a.Group)
                .Include(a => a.Post)
                .ThenInclude(a => a.Category)
                .ToListAsync();
            return View(favorit);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteFavorite(string id)
        {
            var favId = _db.FavoritePosts.FirstOrDefault(a => a.Id == id);
            _db.Remove(favId);
            await _db.SaveChangesAsync();
            return RedirectToAction("MyFavorites");
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
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;

            return RedirectToAction("index", "post", new { id = post.CategoryId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCommentProfile(string postId, string content, Comment comment)
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
            string host = _httpContextAccessor.HttpContext.Request.Host.Value;

            return Redirect("/Identity/Account/Manage");
        }



        private async Task<int> GetTotalPostLikesAsync(string postId)
        {
            return await _db.Likes.Where(l => l.PostId == postId).CountAsync();
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LikePost(string postId, string id)
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

            return RedirectToAction("Index", "Post", new { id = post.CategoryId });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Share(string postId)
        {
            var post = await _postService.GetPostWithUserAsync(postId);
            post.CreatedOn = DateTime.Now;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Post", new { id = post.CategoryId });
        }
    }


}
