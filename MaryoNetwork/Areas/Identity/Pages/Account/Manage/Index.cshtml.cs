﻿using MaryoNetwork.Data;
using MaryoNetwork.Models;
using MaryoNetwork.Models.Friends;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationDbContext _db;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db = db;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "FullName")]
            public string FullName { get; set; }

            [Display(Name = "About me")]
            [MaxLength(400)]
            public string About { get; set; }

            [Display(Name = "Linkedin")]
            public string Linkedin { get; set; }

            [Display(Name = "Facebook")]
            public string Facebook { get; set; }

            [Display(Name = "Country")]
            public string Country { get; set; }

            [Display(Name = "Github")]
            public string Github { get; set; }

            [Display(Name = "Profile Picture")]
            public byte[] ProfilePicture { get; set; }

            [Display(Name = "Cover Picture")]
            public byte[] CoverPicture { get; set; }

            [Display(Name = "UserPosts")]
            public IEnumerable<User> Users { get; set; }

            [Display(Name = "Request friend")]
            public IEnumerable<Friend> FirendRequest { get; set; }

        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var fullName = user.FullName;
            var about = user.About;
            var linkedin = user.Linkedin;
            var country = user.Country;
            var facebbok = user.Facebook;
            var github = user.Github;
            var profilePicture = user.ProfilePicture;
            var coverPicture = user.CoverPicture;
            Username = userName;


            Input = new InputModel
            {
                FullName = fullName,
                About = about,
                Linkedin = linkedin,
                Facebook = facebbok,
                Country = country,
                Github = github,
                PhoneNumber = phoneNumber,
                ProfilePicture = profilePicture,
                CoverPicture = coverPicture,
                Users = await _db.Users
                .Include(p => p.Posts
                .Where(p => p.UserId == user.Id).OrderByDescending(p=>p.CreatedOn))
                .ThenInclude(p => p.Comments)
                .Include(a => a.Posts)
                .ThenInclude(x => x.Category)
                .Include(a => a.Posts)
                .ThenInclude(x => x.Likes)
                .Include(p => p.FriendRequestReceived)
                .Include(p => p.FriendRequestSent)
                .Include(p => p.Groups)
                .Include(p => p.Members)
                .Include(p => p.Images)
                .ToListAsync(),
                FirendRequest = await _db.Friends.Where(a => a.ReceiverId == user.Id).ToListAsync()
                //Posts.Include(c => c.Comments).Include(u => u.User).Where(x => x.UserId == user.Id).Include(x => x.Category).OrderByDescending(y => y.CreatedOn).ToList()
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var fullName = user.FullName;
            var about = user.About;
            var linkedin = user.Linkedin;
            var country = user.Country;
            var github = user.Github;
            var facebook = user.Facebook;
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            if (Input.FullName != fullName)
            {
                user.FullName = Input.FullName;
                await _userManager.UpdateAsync(user);
            }
            if (Input.About != about)
            {
                user.About = Input.About;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Linkedin != linkedin)
            {
                user.Linkedin = Input.Linkedin;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Facebook != facebook)
            {
                user.Facebook = Input.Facebook;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Github != github)
            {
                user.Github = Input.Github;
                await _userManager.UpdateAsync(user);
            }
            if (Input.Country != country)
            {
                user.Country = Input.Country;
                await _userManager.UpdateAsync(user);
            }

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfilePicture = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
