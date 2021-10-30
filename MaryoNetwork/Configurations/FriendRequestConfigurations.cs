using MaryoNetwork.Models.Friends;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Configurations
{
    public class FriendRequestConfigurations : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            //builder
            //    .HasOne(fr => fr.Sender)
            //    .WithMany(u => u.FriendRequestSent)
            //    .HasForeignKey(fr => fr.SenderId);

            //builder
            //   .HasOne(fr => fr.Receiver)
            //   .WithMany(u => u.FriendRequestReceived)
            //   .HasForeignKey(fr => fr.ReceiverId);
        }
    }
}