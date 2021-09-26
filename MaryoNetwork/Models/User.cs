using MaryoNetwork.Models.Friends;
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
        public byte[] ProfilePicture { get; set; }
        public byte[] CoverPicture { get; set; }
        public bool IsDeleted { get; set; } = false;
        public List<FriendRequest> FriendRequestSent { get; set; } = new List<FriendRequest>();

        public List<FriendRequest> FriendRequestReceived { get; set; } = new List<FriendRequest>();
        public List<UserFriend> Friends { get; set; } = new List<UserFriend>();

        public List<UserFriend> OtherFriends { get; set; } = new List<UserFriend>();
    }
}
