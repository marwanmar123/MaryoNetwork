﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Posts
{
    public class FavoritePost
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
    }
}
