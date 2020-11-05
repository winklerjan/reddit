using Reddit.Models;
using System.Collections.Generic;

namespace Reddit.Services.UserServices
{
    public interface IUserService
    {
        public List<User> ReadAllUsers();
        public string Register(string name, string password, string passwordConfirm);
        public bool Login(string name, string password);
        public void Logout(int userId);
        public User ReadUserByName(string name);
        public User ReadUserById(int name);
    }
}
