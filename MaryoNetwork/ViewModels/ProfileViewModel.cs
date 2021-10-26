using MaryoNetwork.Models;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MaryoNetwork.ViewModels
{
    public class ProfileViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool Selected { get; set; }



        
    }
}
