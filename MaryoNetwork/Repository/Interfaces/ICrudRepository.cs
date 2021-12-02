using System.Collections.Generic;
using System.Threading.Tasks;

namespace MaryoNetwork.Repository.Interfaces
{
    public interface ICrudRepository<Item>
    {
        Task<List<Item>> GetItems();
        Task<Item> GetItem(string id);
        Task<Item> AddItem(Item item);
        Task<int> DeleteItem(string id);
        Task UpdateItem(Item item);
    }
}
