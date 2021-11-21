using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Requests
{
    public class Request
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int Like { get; set; }
    }
}
