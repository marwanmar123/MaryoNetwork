using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Groups
{
    public class Group
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public IEnumerable<GroupMembers> Members { get; set; }
    }
}
