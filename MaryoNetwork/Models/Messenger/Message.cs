using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Messenger
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public User FromUser { get; set; }
        public string ToRoomId { get; set; }
        public Room ToRoom { get; set; }
    }
}
