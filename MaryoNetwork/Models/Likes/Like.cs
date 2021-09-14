using MaryoNetwork.Models.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Likes
{
    public class Like
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
        public User LikeBy { get; set; }
        public string LikeById { get; set; }
    }
}
