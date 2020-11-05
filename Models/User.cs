using System.Collections.Generic;

namespace Reddit.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public List<Post> Posts { get; set; }
        public bool UserLogin { get; set; }
        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public User()
        {
        }
    }
}
