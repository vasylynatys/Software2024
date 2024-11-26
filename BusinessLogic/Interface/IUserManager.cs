using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BusinessLogic.Interface
{
    public interface IUserManager
    {
        List<User> GetAllUsers();
        User AddUser(User user);
        User GetUserByCredentials(string username, string password);
        User GetUserByUsername(string username);
        User GetUserById(int userId);
        void UpdatePassword(User user);
        void DeleteUser(int userId);
        int GetCurrentUserId(string username);
    }
}
