using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Messenger
{
    public class Room
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public User Admin { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
