using MaryoNetwork.Data;
using MaryoNetwork.Extensions;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Services.Posts;
using MaryoNetwork.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
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
        public IActionResult Index(string id)
        {
            var list = _db.Posts
                .Include(c => c.Comments
                .Where(c=>c.PostId == c.Post.Id))
                .Include(u => u.User)
                .Where(x => x.CategoryId == id && x.Approved == true)
                .OrderByDescending(y => y.CreatedOn)
                .ToList();
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
                CategoryId = post.CategoryId,
                Approved = false
            };

            
            _db.Add(addPost);
            await _db.SaveChangesAsync();

            return RedirectToAction("index", "post", new { id = post.CategoryId });
            //var rq = _httpContextAccessor.HttpContext.Features.Get<IHttpRequestFeature>();
            //string host = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            //object factory = ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
            //Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
            //return (IActionResult)context;
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
            //var url = _httpContextAccessor.HttpContext?.Request?.GetDisplayUrl();
            //Uri baseUri = new Uri("https://localhost:44306/");
            //Uri myUri = new Uri(baseUri, "post?id=868d3225-1c67-48d2-93d3-9c60ec8772d9");

            //Console.WriteLine(myUri.AbsolutePath);
            //var pathAndQuery = HttpContext.Request.GetEncodedPathAndQuery();
            var url = HttpContext.Request.GetEncodedUrl();
            //var controllerName = ControllerContext.ActionDescriptor.ControllerName;
            //HttpContext context = _httpContextFactory.Create(HttpContext.Features);
            //context.Request.Path = host;
            //var controller = RouteData.Values["post"].ToString();
            //HttpContext.Current.Request.RequestContext.RouteData.Values;
            //var routeValues = _httpContextAccessor.HttpContext.Request.RouteValues.Values;
            //Request.QueryString.Value.Contains("post")
            //string controllerName = HttpContext.Request.RouteValues["controller"].ToString();
            //var id = HttpContext.Request.RouteValues.Keys.Contains("868");
            //var h = Request.GetDisplayUrl();
            //var location = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}");

            //var url = location.AbsoluteUri;
            //if (url.Contains("post"))
            //{
            //    return RedirectToAction("index", "post", new { id = post.CategoryId });
            //    //return redirect("/identity/account/manage");
            //}
            //else
            //{
            //    return Redirect("/identity/account/manage");
            //    //return redirecttoaction("index", "post", new { id = post.categoryid });

            //}
            //"/identity/account/manage"
            //return Redirect();
            //return Redirect(url);
            //return _httpContextAccessor.HttpContext.User.Identity.Name;
            return RedirectToAction("index", "post", new { id = post.CategoryId });
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
            var likeCount =  _db.Likes.Where(l => l.PostId == postId).CountAsync();
            ViewBag.likeCount = likeCount;

            return RedirectToAction("Index", "Post", new { id = post.CategoryId });
        }

        
    }
}
