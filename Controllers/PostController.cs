using Microsoft.AspNetCore.Mvc;
using Reddit.Services;
using Reddit.Services.UserServices;
using Reddit.ViewModels;
using System.Linq;

namespace Reddit.Controllers
{
    [Route("")]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;

        public PostController(IPostService postService, IUserService userService)
        {
            this.postService = postService;
            this.userService = userService;
        }

        [HttpGet("")]
        [HttpGet("index")]
        public IActionResult Index(int? userId, int? pageNum)
        {
            int pageNumConverted;

            if (pageNum.HasValue)
            {
                pageNumConverted = pageNum.Value;
            }
            else
            {
                pageNumConverted = 1;
            }

            var posts = postService.ReadAllPosts(pageNumConverted);

            var topics = postService.ReadTopics();
            var model = new IndexViewModel() { Posts = posts, Topics = topics };

            if (userId.HasValue)
            {
                var user = userService.ReadUserById(userId.Value);
                model.User = user;
            }

            return View(model);
        }

        [HttpPost("submit")]
        public IActionResult Submit(string title, string url, int userId, int? topicId)
        {
            if (topicId.HasValue)
            {
                postService.Submit(title, url, userId, topicId.Value);
            }
            else
            {
                postService.Submit(title, url, userId, null);
            }


            return RedirectToAction("index", new { UserId = userId });
        }

        [HttpGet("vote")]
        public IActionResult Vote(int postId, int userId, string vote)
        {
            postService.Vote(postId, vote);
            return RedirectToAction("index", new { UserId = userId });
        }

        [HttpGet("edit")]
        public IActionResult Edit(int postId, int userId)
        {
            var post = postService.ReadPostById(postId);
            var topics = postService.ReadTopics();
            var user = userService.ReadUserById(userId);
            var model = new IndexViewModel() { Post = post, Topics = topics, User = user };

            return View(model);
        }

        [HttpPost("edit")]
        public IActionResult Edit(int postId, string title, string url)
        {
            var user = userService.ReadAllUsers().First(u => u.UserLogin == true);
            postService.Edit(postId, title, url);

            return RedirectToAction("profile", "user", new { UserId = user.Id });
        }

        [HttpGet("delete")]
        public IActionResult Delete(int postId, int userId)
        {
            postService.Delete(postId);
            return RedirectToAction("profile", "user", new { UserId = userId });
        }
    }
}
