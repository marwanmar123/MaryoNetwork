using MaryoNetwork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

//            builder
//                .HasMany(u => u.Friends)
//                .WithOne(uf => uf.User)
//                .HasForeignKey(uf => uf.UserId);

//            builder
//               .HasMany(u => u.OtherFriends)
//               .WithOne(uf => uf.Friend)
//               .HasForeignKey(uf => uf.FriendId);
//;
        }
    }
}
