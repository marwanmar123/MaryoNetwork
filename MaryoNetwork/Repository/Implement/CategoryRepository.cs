using MaryoNetwork.Data;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Repository.Implement
{
    public class CategoryRepository : ICrudRepository<Category>
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Category> AddItem(Category category)
        {
            await _db.Categories.AddAsync(category);

            await _db.SaveChangesAsync();
            return category;
        }

        public async Task<int> DeleteItem(string id)
        {
            var user = await _db.Categories
                .Include(a=>a.Post)
                .ThenInclude(a=>a.Comments)
                .Include(a => a.Post)
                .ThenInclude(a => a.Likes)
                .Include(a => a.Post)
                .ThenInclude(a => a.Images)
                .FirstOrDefaultAsync(i => i.Id == id);
            foreach(var p in user.Post)
            {
                _db.Remove(p);
                foreach (var i in p.Images)
                {
                    _db.Remove(i);
                }
                foreach (var c in p.Comments)
                {
                    _db.Remove(c);
                }
                foreach (var l in p.Likes)
                {
                    _db.Remove(l);
                }
            }
            _db.Categories.Remove(user);
            var save = await _db.SaveChangesAsync();
            return save;
        }

        public async Task<Category> GetItem(string id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task<List<Category>> GetItems()
        {
            return await _db.Categories.ToListAsync();
        }

        public async Task UpdateItem(Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }
    }
}
