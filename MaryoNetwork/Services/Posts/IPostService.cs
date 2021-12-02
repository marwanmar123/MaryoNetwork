using MaryoNetwork.Models.Posts;
using System.Threading.Tasks;

namespace MaryoNetwork.Services.Posts
{
    public interface IPostService
    {
        Task<Post> GetPostWithUserAsync(string postId);
    }
}
