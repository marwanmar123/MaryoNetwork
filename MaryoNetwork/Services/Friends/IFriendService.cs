using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Services.Friends
{
    public interface IFriendService
    {
        void Create(string senderId, string receiverId);

        void Accept(string senderId, string receiverId);

        void Delete(string senderId, string receiverId);

        void Decline(string senderId, string receiverId);

        bool Exists(string senderId, string receiverId);
    }
}
