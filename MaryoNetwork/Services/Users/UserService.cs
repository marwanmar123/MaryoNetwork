using MaryoNetwork.Data;
using MaryoNetwork.Models;
using MaryoNetwork.Models.Friends;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Services.Users
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _accessor;

        public UserService(ApplicationDbContext db, UserManager<User> userManager, IHttpContextAccessor accessor)
        {
            _db = db;
            _userManager = userManager;
            _accessor = accessor;
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            return (User)await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetUserByUserNameAsync(string username)
        {
            return (User)await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<string> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(_accessor.HttpContext.User);

            return await _userManager.GetUserIdAsync(user);
        }

        public async Task<string> GetCurrentUserNameAsync()
        {
            var user = await _userManager.GetUserAsync(_accessor.HttpContext.User);

            return await _userManager.GetUserNameAsync(user);
        }

        public async Task<IQueryable<User>> GetAllUsersExceptCurrentUser()
        {
            var currentUserId = await GetCurrentUserIdAsync();

            return (IQueryable<User>)_userManager.Users.Where(u => u.Id != currentUserId);
        }


        public bool UserExists(string userId) => _db.Users.Any(u => u.Id == userId && u.IsDeleted == false);

    }
}
