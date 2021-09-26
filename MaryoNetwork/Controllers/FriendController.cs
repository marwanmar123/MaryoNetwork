using MaryoNetwork.Extensions;
using MaryoNetwork.Services.Friends;
using MaryoNetwork.Services.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    public class FriendController : Controller
    {
        private readonly IFriendService _friendService;
        private readonly IUserService _userService;

        public FriendController(IFriendService friendService, IUserService userService)
        {
            _friendService = friendService;
            _userService = userService;
        }
        
        public IActionResult AddFriend(string senderId, string receiverId)
        {
            if (!_userService.UserExists(senderId) || !_userService.UserExists(receiverId) || senderId != this.User.GetUserId())
            {
                return NotFound();
            }

            this._friendService.Create(senderId, receiverId);

            return RedirectToAction("AccountDetails", "Users", new { id = receiverId });
        }

        public IActionResult Accept(string senderId, string receiverId)
        {
            this._friendService.Accept(senderId, receiverId);
            return RedirectToAction("AccountDetails", "Users", new { id = senderId });
        }

        public IActionResult Decline(string senderId, string receiverId)
        {
            this._friendService.Decline(senderId, receiverId);
            return RedirectToAction("AccountDetails", "Users", new { id = senderId });
        }
    }
}
