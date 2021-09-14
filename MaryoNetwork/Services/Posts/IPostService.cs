using MaryoNetwork.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Services.Posts
{
    public interface IPostService
    {
        Task<Post> GetPostWithUserAsync(string postId);
    }
}
