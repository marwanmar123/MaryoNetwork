﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Friends
{
    public class Friend
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public string SenderId { get; set; }

        public User Sender { get; set; }

        public string ReceiverId { get; set; }

        public User Receiver { get; set; }

        public string RequestStatusId { get; set; }
        public RequestStatus RequestStatus { get; set; }
    }
}