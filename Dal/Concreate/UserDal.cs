using DTO;
using Dal.Interface;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace Dal.Concreate
{
    public class UserDal : IUserDal
    {
        private readonly SqlConnection _connectionString;

        public UserDal(string connectionString)
        {
            _connectionString = new SqlConnection(connectionString);
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        private string HashPassword(string password, string salt)
        {
            using (var sha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(salt + password);
                byte[] hashBytes = sha512.ComputeHash(passwordBytes);
                return salt + Convert.ToBase64String(hashBytes);
            }
        }
        public User GetByUsername(string username)
        {
            User user = null;

            using (SqlCommand command = _connectionString.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE Username = @Username";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Username", username);

                try
                {
                    _connectionString.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = (int)reader["UserID"],
                                Username = reader["Username"].ToString(),
                                Password = reader["Password"].ToString(),
                            };
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"SQL error: {ex.Message}");
                }
                finally
                {
                    _connectionString.Close();
                }
            }

            return user;
        }
        public User GetUserByCredentials(string username, string password)
        {
            var user = GetByUsername(username);
            if (user == null)
            {

                Console.WriteLine($"User not found: {username}");
                return null;
            }

            if (!verifyPassword(password, user.Password))
            {

                Console.WriteLine($"Invalid password for user: {username}");
                return null;
            }

            return user;
        }
        public bool verifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string salt = storedHashedPassword.Substring(0, 24);
            string hashedInputPassword = HashPassword(enteredPassword, salt);
            return hashedInputPassword == storedHashedPassword;
        }
        public List<User> GetAll()
        {
            var users = new List<User>();
            using (SqlCommand command = _connectionString.CreateCommand())
            {
                command.CommandText = "SELECT UserID, Username, Password FROM Users";

                _connectionString.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserId = Convert.ToInt32(reader["UserID"]),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                        });
                    }
                }
                _connectionString.Close();
            }
            return users;
        }
        public User Insert(User user)
        {
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(user.Password, salt);

            using (SqlCommand command = _connectionString.CreateCommand())
            {
                command.CommandText = "INSERT INTO Users (Username, Password) OUTPUT INSERTED.UserID VALUES (@Username, @Password)";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Username", user.Username);
                command.Parameters.AddWithValue("@Password", hashedPassword);

                _connectionString.Open();
                user.UserId = Convert.ToInt32(command.ExecuteScalar());
                _connectionString.Close();
                return user;
            }
        }
        public void UpdatePassword(User user)
        {
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(user.Password, salt);

            using (SqlCommand command = _connectionString.CreateCommand())
            {
                command.CommandText = "UPDATE Users SET Password = @Password WHERE UserID = @UserID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@Password", hashedPassword);
                command.Parameters.AddWithValue("@UserID", user.UserId);

                _connectionString.Open();
                command.ExecuteNonQuery();
                _connectionString.Close();
            }
        }
        public void Delete(int userId)
        {
            using (SqlCommand command = _connectionString.CreateCommand())
            {
                command.CommandText = "DELETE FROM Users WHERE UserID = @UserID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", userId);

                _connectionString.Open();
                command.ExecuteNonQuery();
                _connectionString.Close();
            }
        }
        public User GetById(int userId)
        {
            User user = null;

            using (SqlCommand command = _connectionString.CreateCommand())
            {
                command.CommandText = "SELECT * FROM Users WHERE UserID = @UserID";
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@UserID", userId);

                _connectionString.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            UserId = (int)reader["UserID"],
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                        };
                    }
                }
                _connectionString.Close();
            }

            return user;
        }
    }
}
}
