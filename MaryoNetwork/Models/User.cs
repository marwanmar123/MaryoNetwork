using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Friends;
using MaryoNetwork.Models.Groups;
using MaryoNetwork.Models.Images;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Messenger;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Models.Requests;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MaryoNetwork.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }
        public string Country { get; set; }
        public string Github { get; set; }
        public byte[] ProfilePicture { get; set; }
        public byte[] CoverPicture { get; set; }
        public bool IsOnline { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Request> Requests { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<GroupMembers> Members { get; set; }
        public ICollection<Friend> FriendRequestSent { get; set; }
        public ICollection<Friend> FriendRequestReceived { get; set; }
    }
}
