using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Groups;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Models.Requests;
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
        public List<Request> Request { get; set; }
        public List<Group> Group { get; set; }
    }
}
