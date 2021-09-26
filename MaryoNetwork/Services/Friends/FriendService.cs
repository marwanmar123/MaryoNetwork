using MaryoNetwork.Data;
using MaryoNetwork.Models.Friends;
using MaryoNetwork.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Services.Friends
{
    public class FriendService : IFriendService
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserService _userService;

        public FriendService(ApplicationDbContext db, IUserService userService)
        {
            _db = db;
            _userService = userService;
        }

        public void Accept(string senderId, string receiverId)
        {
            if (this.Exists(senderId, receiverId) && _userService.UserExists(senderId) && _userService.UserExists(receiverId))
            {
                var friendRequest = _db.FriendRequests.FirstOrDefault(fr => fr.ReceiverId == receiverId && fr.SenderId == senderId);
                friendRequest.FriendRequestStatus = FriendRequestStatus.Accepted;
                _userService.MakeFriends(senderId, receiverId);
                _db.SaveChanges();
            }
        }

        public void Create(string senderId, string receiverId)
        {
            if (!this.Exists(senderId, receiverId) && _userService.UserExists(senderId) && _userService.UserExists(receiverId))
            {
                var friendRequest = new FriendRequest
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    FriendRequestStatus = FriendRequestStatus.Pending
                };

                _db.FriendRequests.Add(friendRequest);
                _db.SaveChanges();
            }
        }

        public void Decline(string senderId, string receiverId)
        {
            if (this.Exists(senderId, receiverId) && _userService.UserExists(senderId) && _userService.UserExists(receiverId))
            {
                var friendRequest = _db.FriendRequests.FirstOrDefault(fr => fr.ReceiverId == receiverId && fr.SenderId == senderId);
                _db.Remove(friendRequest);
                _db.SaveChanges();
            }
        }

        public void Delete(string senderId, string receiverId)
        {
            throw new System.NotImplementedException();
        }

        public bool Exists(string senderId, string receiverId) =>
             _db.FriendRequests.Any(fr => fr.SenderId == senderId && fr.ReceiverId == receiverId);
    }
}
