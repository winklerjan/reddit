using Reddit.Database;
using Reddit.Models;
using System.Collections.Generic;
using System.Linq;

namespace Reddit.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<User> ReadAllUsers()
        {
            return dbContext.Users.ToList();
        }

        public void LogoutAllUsers(string name)
        {
            foreach (var user in dbContext.Users)
            {
                if (user.Name != name)
                {
                    user.UserLogin = false;
                }
            }
        }

        public bool CheckIfUserExists(string name)
        {
            return ReadAllUsers().Any(u => u.Name == name);
        }

        public string Register(string name, string password, string passwordConfirm)
        {
            LogoutAllUsers(name);

            if (password != passwordConfirm)
            {
                return "passwordError";
            }
            else if (CheckIfUserExists(name))
            {
                return "userExistsError";
            }
            else
            {
                var newUser = new User(name, password);
                dbContext.Users.Add(newUser);
                newUser.UserLogin = true;
                dbContext.SaveChanges();
                return "success";
            }
        }

        public User ReadUserByName(string name)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Name == name);
            return user;
        }
        public User ReadUserById(int id)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public bool Login(string name, string password)
        {
            LogoutAllUsers(name);
            var users = ReadAllUsers();

            try
            {
                var user = users
                    .First(u => u.Name == name && u.Password == password)
                    .UserLogin = true;
                dbContext.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public void Logout(int userId)
        {
            ReadAllUsers().FirstOrDefault(u => u.Id == userId).UserLogin = false;
            dbContext.SaveChanges();
        }
    }
}