using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Friends
{
    public enum FriendRequestStatus
    {
        Pending = 0,
        Accepted = 1,
        Declined = 2,
        Blocked = 3
    }
}
