using Xunit;
using Moq;
using AwesomeApp.Services;
using AwesomeApp.Models;
using AwesomeApp.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MobileAppTests
{
    public class UserRegisterVMTest
    {

        [Fact]
        public void FillFirstNameShouldRaisePropertyChange()
        {
            // Arrange
            bool invoked = false;
            var userVm = new UserRegisterViewModel(new Mock<IUserService>().Object);

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
            var userVm = new UserRegisterViewModel(new Mock<IUserService>().Object);

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
            var userVm = new UserRegisterViewModel(new Mock<IUserService>().Object);

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
    
        
    }
}
