using Moq;
using UserManagement.Application;
using UserManagement.Core.Repositories;
using UserManagement.Core;
using Xunit;

namespace UserManagement.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void GetAllUsers_ReturnsUserList()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "John", Surname = "Doe", Cellphone = "123456789" }
            };
            _userRepositoryMock.Setup(repo => repo.GetAllUsers()).Returns(users);

            // Act
            var result = _userService.GetAllUsers();

            // Assert
            Assert.Single(result);
            Assert.Equal("John", result[0].Name);
        }

        [Fact]
        public void AddUser_CallsRepositoryAdd()
        {
            // Arrange
            var user = new User { Id = Guid.NewGuid(), Name = "Jane", Surname = "Smith", Cellphone = "987654321" };

            // Act
            _userService.AddUser(user);

            // Assert
            _userRepositoryMock.Verify(r => r.AddUser(It.Is<User>(u => u.Name == "Jane")), Times.Once);
        }

        [Fact]
        public void DeleteUser_CallsRepositoryDelete()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            _userService.DeleteUser(userId);

            // Assert
            _userRepositoryMock.Verify(r => r.DeleteUser(userId), Times.Once);
        }
    }
}
