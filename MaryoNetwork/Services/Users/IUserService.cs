﻿using MaryoNetwork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Services.Users
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string userId);

        Task<User> GetUserByUserNameAsync(string username);

        Task<string> GetCurrentUserIdAsync();

        Task<string> GetCurrentUserNameAsync();

        Task<IQueryable<User>> GetAllUsersExceptCurrentUser();

        bool UserExists(string userId);

        void MakeFriends(string senderId, string receiverId);

        bool CheckIfFriends(string requestUserId, string targetUserId);
    }
}