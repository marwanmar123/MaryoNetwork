using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Friends;
using MaryoNetwork.Models.Images;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Posts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public byte[] ProfilePicture { get; set; }
        public byte[] CoverPicture { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Post> Posts { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        //public List<User> FriendsUser { get; set; } = new List<User>();
        //public IEnumerable<FriendRequest> FriendRequestSent { get; set; } = new List<FriendRequest>();

        //public IEnumerable<FriendRequest> FriendRequestReceived { get; set; } = new List<FriendRequest>();
        //public IEnumerable<UserFriend> Friends { get; set; } = new List<UserFriend>();

        //public IEnumerable<UserFriend> OtherFriends { get; set; } = new List<UserFriend>();


        
    }
}
