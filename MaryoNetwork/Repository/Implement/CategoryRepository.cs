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
            var user = await _db.Categories.FirstOrDefaultAsync(i => i.Id == id);
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
