using MaryoNetwork.Models.Posts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Categories
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Post { get; set; }
    }
}
