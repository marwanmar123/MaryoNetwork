using MaryoNetwork.Data;
using MaryoNetwork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db;

        public SideBarViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            //CategoryPostViewModel dataBar = new CategoryPostViewModel()
            //{
            //    Categories =await _db.Categories.ToListAsync(),
            //    Posts = await _db.Posts.ToListAsync()
            //};
            var dataBar = await _db.Categories.ToListAsync();
            return View(dataBar);
        }
    }
}
