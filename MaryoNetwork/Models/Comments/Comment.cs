using MaryoNetwork.Models.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Comments
{
    public class Comment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public User User { get; set; }
        public Post Post { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
