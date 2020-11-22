using Reddit.Models;
using System.Collections.Generic;

namespace Reddit.ViewModels
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public Post Post { get; set; }
        public List<Post> Posts { get; set; }
        public List<Topic> Topics { get; set; }

        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPostCount { get; set; }

        public IndexViewModel()
        {

        }
    }
}
