using BusinessLogic.Concrete;
using Dal.Interface;
using DTO;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace BusinessLogicTests
{
    [TestFixture]
    public class UserManagerTests
    {
        private UserManager _userManager;
        private Mock<IUserDal> _mockUserDal;

        [SetUp]
        public void Setup()
        {
            _mockUserDal = new Mock<IUserDal>();
            _userManager = new UserManager(_mockUserDal.Object);
        }

        [Test]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            // Arrange
            var expectedUsers = new List<User>
            {
                new User { UserId = 1, Username = "User1", Password = "Password1" },
                new User { UserId = 2, Username = "User2", Password = "Password2" }
            };
            _mockUserDal.Setup(dal => dal.GetAll()).Returns(expectedUsers);

            // Act
            var actualUsers = _userManager.GetAllUsers();

            // Assert
            Assert.AreEqual(expectedUsers.Count, actualUsers.Count, "GetAllUsers should return the correct number of users.");
            Assert.AreEqual(expectedUsers[0].Username, actualUsers[0].Username, "GetAllUsers should return the correct users.");
        }

        [Test]
        public void GetUserById_ShouldReturnCorrectUser()
        {
            // Arrange
            var userId = 1;
            var expectedUser = new User { UserId = userId, Username = "User1", Password = "Password1" };
            _mockUserDal.Setup(dal => dal.GetById(userId)).Returns(expectedUser);

            // Act
            var actualUser = _userManager.GetUserById(userId);

            // Assert
            Assert.IsNotNull(actualUser, "GetUserById should return a user.");
            Assert.AreEqual(expectedUser.Username, actualUser.Username, "GetUserById should return the correct user.");
        }

        [Test]
        public void AddUser_ShouldReturnAddedUser()
        {
            // Arrange
            var newUser = new User { Username = "NewUser", Password = "NewPassword" };
            var addedUser = new User { UserId = 1, Username = "NewUser", Password = "NewPassword" };
            _mockUserDal.Setup(dal => dal.Insert(newUser)).Returns(addedUser);

            // Act
            var result = _userManager.AddUser(newUser);

            // Assert
            Assert.IsNotNull(result, "AddUser should return the added user.");
            Assert.AreEqual(addedUser.UserId, result.UserId, "AddUser should return the user with the correct ID.");
        }

        [Test]
        public void DeleteUser_ShouldCallDalDeleteMethod()
        {
            // Arrange
            var userId = 1;

            // Act
            _userManager.DeleteUser(userId);

            // Assert
            _mockUserDal.Verify(dal => dal.Delete(userId), Times.Once, "DeleteUser should call the Dal's Delete method exactly once.");
        }
    }
}
