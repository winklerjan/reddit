using Microsoft.EntityFrameworkCore;
using Reddit.Database;
using Reddit.Models;
using System.Collections.Generic;
using System.Linq;

namespace Reddit.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int PageSize { get; set; } = 10;

        public List<Post> ReadAllPosts(int pageNum)
        {
            return dbContext.Posts.Include(p => p.User)
                .OrderByDescending(p => p.Points)
                .ThenByDescending(p => p.Created)
                .Skip((pageNum - 1) * PageSize)
                .Take(PageSize)
                .ToList();
        }

        public List<Topic> ReadTopics()
        {
            return dbContext.Topics.ToList();
        }

        public List<Post> ReadAllPostsByUserID(int userID)
        {
            return dbContext.Posts.Where(u => u.UserID == userID).ToList();
        }

        public Post ReadPostById(int postId)
        {
            return dbContext.Posts.FirstOrDefault(p => p.ID == postId);
        }

        public void Submit(string title, string url, int userId, int? topicId)
        {
            var newPost = new Post(title, url, userId, topicId);
            dbContext.Posts.Add(newPost);
            dbContext.SaveChanges();
        }

        public void Vote(int postId, string vote)
        {
            switch (vote)
            {
                case "up":
                    ReadPostById(postId).Points++;
                    break;

                case "down":
                    ReadPostById(postId).Points--;
                    break;
            }

            dbContext.SaveChanges();
        }

        public void Edit(int postId, string title, string url)
        {
            var post = ReadPostById(postId);
            post.Title = title;
            post.Url = url;
            if (post.Points > 0)
            {
                post.Points = 0;
            }

            dbContext.SaveChanges();
        }

        public void Delete(int postId)
        {
            dbContext.Posts.Remove(ReadPostById(postId));
            dbContext.SaveChanges();
        }
    }
}
