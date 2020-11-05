using Microsoft.AspNetCore.Mvc;
using Reddit.Services;
using Reddit.Services.UserServices;
using Reddit.ViewModels;

namespace Reddit.Controllers
{
    [Route("")]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IPostService postService;
        public UserController(IUserService userService, IPostService postService)
        {
            this.userService = userService;
            this.postService = postService;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(string name, string password, string passwordConfirm)
        {
            var userRegistration = userService.Register(name, password, passwordConfirm);
            var user = userService.ReadUserByName(name);

            switch (userRegistration)
            {
                case "passwordError":
                    return View("RegistrationPasswordError");
                case "userExistsError":
                    return View("RegistrationUserError");
                default:
                    return RedirectToAction("Profile", new { UserId = user.Id });
            }
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public IActionResult Login(string name, string password)
        {
            var userLogin = userService.Login(name, password);
            var user = userService.ReadUserByName(name);

            if (!userLogin)
            {
                return View("LoginError");
            }
            return RedirectToAction("Profile", new { UserId = user.Id });
        }

        [HttpGet("logout")]
        public IActionResult Logout(int userId)
        {
            userService.Logout(userId);
            return View();
        }

        [HttpGet("profile")]
        public IActionResult Profile(int userId)
        {
            var posts = postService.ReadAllPostsByUserID(userId);
            var topics = postService.ReadTopics();
            var user = userService.ReadUserById(userId);

            var model = new IndexViewModel() { User = user, Posts = posts, Topics = topics };

            return View("Profile", model);
        }
    }
}
