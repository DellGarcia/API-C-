using Xunit;
using Moq;
using AwesomeApp.Services;
using AwesomeApp.Models;
using System.Net.Http;

namespace MobileAppTests
{
    public class UserServiceTest
    {
        [Fact]
        public void IsUserFieldsValid_IfFirstNameIsNull_ReturnsFalse()
        {
            // Arrage
            User user = new()
            {
                FirstName = null,
                SurName = "sobrenome",
                Age = 29
            };

            UserService userService = new(new Mock<HttpClient>().Object);

            // Act
            var result = userService.IsUserFieldsValid(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsUserFieldsValid_IfFisrtNameIsEmpty_ReturnsFalse()
        {
            // Arrage
            User user = new()
            {
                FirstName = "",
                SurName = "sobrenome",
                Age = 29
            };

            UserService userService = new(new Mock<HttpClient>().Object);

            // Act
            var result = userService.IsUserFieldsValid(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsUserFieldsValid_IfAgeIsNull_ReturnsFalse()
        {
            // Arrage
            User user = new()
            {
                FirstName = "nome",
                SurName = "sobrenome",
                Age = null
            };

            UserService userService = new(new Mock<HttpClient>().Object);

            // Act
            var result = userService.IsUserFieldsValid(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsUserFieldsValid_IfAgeIsInvalid_ReturnsFalse()
        {
            // Arrage
            User user = new()
            {
                FirstName = "nome",
                SurName = "sobrenome",
                Age = 1
            };

            UserService userService = new(new Mock<HttpClient>().Object);

            // Act
            var result = userService.IsUserFieldsValid(user);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsUserFieldsValid_IfUserIsValid_ReturnsTrue()
        {
            // Arrage
            User user = new()
            {
                FirstName = "nome",
                SurName = "sobrenome",
                Age = 25
            };

            UserService userService = new(new Mock<HttpClient>().Object);

            // Act
            var result = userService.IsUserFieldsValid(user);

            // Assert
            Assert.True(result);
        }
    }
}
