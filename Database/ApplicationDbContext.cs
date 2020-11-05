using Microsoft.EntityFrameworkCore;
using Reddit.Models;

namespace Reddit.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasMany<Post>(u => u.Posts)
               .WithOne(p => p.User)
               .HasForeignKey(p => p.UserID);

            modelBuilder.Entity<Topic>()
                .HasMany<Post>(t => t.Posts)
                .WithOne(p => p.Topic)
                .HasForeignKey(p => p.TopicID);
        }
    }
}
