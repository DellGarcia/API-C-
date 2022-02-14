using Xunit;
using Moq;
using AwesomeApp.Services;
using AwesomeApp.Models;
using AwesomeApp.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace MobileAppTests
{
    public class UserSaveVMTest
    {
        [Fact]
        public void FillFirstNameShouldRaisePropertyChange()
        {
            // Arrange
            bool invoked = false;
            var userVm = new UserSaveViewModel(new Mock<IUserService>().Object);

            userVm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("FirstName"))
                    invoked = true;
            };

            // Act
            userVm.FirstName = "Nome";

            // Assert
            Assert.True(invoked);
        }

        [Fact]
        public void FillSurnameShouldRaisePropertyChange()
        {
            // Arrange
            bool invoked = false;
            var userVm = new UserSaveViewModel(new Mock<IUserService>().Object);

            userVm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Surname"))
                    invoked = true;
            };

            // Act
            userVm.Surname = "Sobrenome";

            // Assert
            Assert.True(invoked);
        }

        [Fact]
        public void FillAgeShouldRaisePropertyChange()
        {
            // Arrange
            bool invoked = false;
            var userVm = new UserSaveViewModel(new Mock<IUserService>().Object);

            userVm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("Age"))
                    invoked = true;
            };

            // Act
            userVm.Age = 35;

            // Assert
            Assert.True(invoked);
        }
    
        [Fact]
        public void FindUserShouldRaisePropertyChange()
        {
            // Arrange
            bool invoked = false;
            User user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Nome",
                SurName = "Sobrenome",
                Age = 55
            };

            Mock<IUserService> service = new Mock<IUserService>();
            service.Setup(e => e.GetUser(It.IsAny<Guid>()))
                .Returns(Task<User>.Factory.StartNew(() => user));

            var userVm = new UserSaveViewModel(service.Object);
            userVm._user = user;

            userVm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("FirstName") || e.PropertyName.Equals("Age"))
                    invoked = true;
            };

            // Act
            userVm.FindUser();

            // Assert
            Assert.True(invoked);
        }
    }
}
