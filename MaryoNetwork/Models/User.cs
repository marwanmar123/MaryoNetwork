using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Friends;
using MaryoNetwork.Models.Images;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Messenger;
using MaryoNetwork.Models.Posts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public bool IsOnline { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Friend> FriendRequestSent { get; set; }
        public ICollection<Friend> FriendRequestReceived { get; set; }
    }
}
