using MaryoNetwork.Models.Friends;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MaryoNetwork.Data.Configurations
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder
                .HasOne(fr => fr.Sender)
                .WithMany(u => u.FriendRequestSent)
                .HasForeignKey(fr => fr.SenderId);

            builder
               .HasOne(fr => fr.Receiver)
               .WithMany(u => u.FriendRequestReceived)
               .HasForeignKey(fr => fr.ReceiverId);
        }
    }
}