using MaryoNetwork.Data.Configurations;
using MaryoNetwork.Models;
using MaryoNetwork.Models.Categories;
using MaryoNetwork.Models.Comments;
using MaryoNetwork.Models.Editors;
using MaryoNetwork.Models.Friends;
using MaryoNetwork.Models.Groups;
using MaryoNetwork.Models.Images;
using MaryoNetwork.Models.Likes;
using MaryoNetwork.Models.Messenger;
using MaryoNetwork.Models.Posts;
using MaryoNetwork.Models.Requests;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MaryoNetwork.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<RequestStatus> RequestStatus { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMembers> GroupMembers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<FavoritePost> FavoritePosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new FriendConfiguration());

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
