using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Friends
{
    public class FriendRequest
    {
        public string Id { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        //[Required]
        public FriendRequestStatus FriendRequestStatus { get; set; }
    }
}
