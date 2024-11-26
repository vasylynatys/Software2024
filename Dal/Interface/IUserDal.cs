using DTO;
using System.Reflection.Metadata;

namespace Dal.Interface
{
    public interface IUserDal
    {
        List<User> GetAll();
        User Insert(User user);
        User GetUserByCredentials(string username, string password);
        User GetByUsername(string username);
        void UpdatePassword(User user);
        void Delete(int userId);
        User GetById(int id);
        bool verifyPassword(string enteredPassword, string storedHashedPassword);
    }
}
