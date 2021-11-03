using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Groups
{
    public class GroupMembers
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string GroupId { get; set; }
        public Group Group { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
