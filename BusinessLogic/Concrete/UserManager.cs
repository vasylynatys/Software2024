using BusinessLogic.Interface;
using Dal.Interface;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
    public class UserManager : IUserManager
    {
        private readonly IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public List<User> GetAllUsers()
        {
            return _userDal.GetAll();
        }
        public User GetUserById(int id)
        {
            return _userDal.GetById(id);
        }
        public User GetUserByUsername(string username) 
        { 
            return _userDal.GetByUsername(username);
        }
        public User GetUserByCredentials(string username, string password)
        {
            return _userDal.GetUserByCredentials(username, password);
        }
        public User AddUser(User user)
        {
            return _userDal.Insert(user);
        }
        public void UpdatePassword(User user) 
        {
            _userDal.UpdatePassword(user);
        }
        public void DeleteUser(int userId)
        {
            _userDal.Delete(userId);
        }
        public int GetCurrentUserId(string username)
        {
            var user = GetUserByUsername(username);
            return user?.UserId ?? 0;
        }

    }
}
