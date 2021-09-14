using MaryoNetwork.Data;
using MaryoNetwork.Models.Posts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _db;

        public PostService(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<Post> GetPostWithUserAsync(string postId)
        {
            return _db.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == postId);
        }
    }
}
