using MaryoNetwork.Models;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public IEnumerable<Post> Post { get; set; }
        public IEnumerable<Comment> comment { get; set; }
        public IEnumerable<Like> Like { get; set; }
    }
}
