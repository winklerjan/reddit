using System;
using System.ComponentModel.DataAnnotations;

namespace Reddit.Models
{
    public class Post
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public User User { get; set; }
        public int? UserID { get; set; }
        public Topic Topic { get; set; }
        public int? TopicID { get; set; }
        public long Points { get; set; }

        public Post()
        {
            Created = DateTime.Now;
        }

        public Post(string title, string url, int userId, int topicId)
        {
            Title = title;
            Url = url;
            Created = DateTime.Now;
            UserID = userId;
            TopicID = topicId;
        }
    }
}
