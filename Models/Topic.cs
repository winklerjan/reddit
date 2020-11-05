using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reddit.Models
{
    public class Topic
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }

        public Topic()
        {

        }
    }
}
