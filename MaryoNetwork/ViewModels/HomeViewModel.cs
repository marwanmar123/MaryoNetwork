using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Posts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.ViewModels
{
    public class HomeViewModel
    {
        public List<Category> Category { get; set; }
        public List<Post> Post { get; set; }
    }
}
