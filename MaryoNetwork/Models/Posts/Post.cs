using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Groups;
using MaryoNetwork.Models.Images;
using MaryoNetwork.Models.Likes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Posts
{
    public class Post
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ICollection<Image> Images { get; set; }
        public bool Approved { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public IEnumerable<Like> Likes { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
    }
}
