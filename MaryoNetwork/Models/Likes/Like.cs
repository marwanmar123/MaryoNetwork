using MaryoNetwork.Models.Posts;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Likes
{
    public class Like
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public Post Post { get; set; }
        public User User { get; set; }
    }
}
