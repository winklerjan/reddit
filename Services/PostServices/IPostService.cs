using Reddit.Models;
using System.Collections.Generic;

namespace Reddit.Services
{
    public interface IPostService
    {
        public List<Post> ReadAllPosts(int begin);
        public List<Post> ReadAllPostsByUserID(int userId);
        public void Submit(string title, string url, int userId, int? topicId);
        public void Vote(int postId, string vote);
        public Post ReadPostById(int postId);
        public List<Topic> ReadTopics();
        public void Edit(int postId, string title, string url);
        public void Delete(int postId);
        public void AddTopic(string topic);
        public int GetAllPostsCount();

    }
}
