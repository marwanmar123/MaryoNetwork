﻿using MaryoNetwork.Models.Posts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Groups
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<GroupMembers> Members { get; set; }
        public ICollection<Post> Post { get; set; }
    }
}
