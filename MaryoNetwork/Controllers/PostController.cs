using MaryoNetwork.Data;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Services.Posts;
using MaryoNetwork.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(ApplicationDbContext db,
            IPostService postService,
            IUserService userService)
        {
            _db = db;
            _postService = postService;
            _userService = userService;
        }








        [Authorize]
        public IActionResult Index(string id)
        {
            var list = _db.Posts.Include(c => c.Comments).Include(u => u.User).Where(x => x.CategoryId == id).OrderByDescending(y => y.CreatedOn).ToList();
            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(Post post, string postId)
        {
            var postt = await _postService.GetPostWithUserAsync(postId);
            var totalLike = _db.Likes.Where(l => l.PostId == postId);

            var addPost = new Post
            {
                Title = post.Title,
                Image = post.Image,
                Content = post.Content,
                UserId = await _userService.GetCurrentUserIdAsync(),
                CategoryId = post.CategoryId
            };

            _db.Add(addPost);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Post", new { id = post.CategoryId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateComment(string postId, string content)
        {
            var post = await _postService.GetPostWithUserAsync(postId);



            var addComment = new Comment
            {
                Content = content,
                UserId = await _userService.GetCurrentUserIdAsync(),
                PostId = post.Id
            };
            await _db.AddAsync(addComment);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Post", new { id = post.CategoryId });
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

            var like = _db.Likes.FirstOrDefault(l => l.PostId == postId && l.LikeById == userId);



            if (like == null)
            {
                like = new Like
                {
                    LikeById = userId,
                    PostId = post.Id
                };
                await _db.AddAsync(like);
            }
            else
            {
                _db.Likes.Remove(like);
            }
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Post", new { id = post.CategoryId });
        }
    }
}
