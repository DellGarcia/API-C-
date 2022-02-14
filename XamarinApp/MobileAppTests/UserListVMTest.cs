using Xunit;
using Moq;
using AwesomeApp.Services;
using AwesomeApp.Models;
using AwesomeApp.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MobileAppTests
{
    public class UserListVMTest
    {

        [Fact]
        public void GetUsers_SetTheUsersInAllUsersCollection()
        {
            // arrange
            Mock<IUserService> service = new();
            service.Verify();
            IEnumerable<User> users = new List<User>()
            {
                new User(),
                new User(),
                new User()
            };

            service.Setup(e => e.GetUsers())
                .Returns(Task<IEnumerable<User>>.Factory.StartNew(() => users));

            // act
            UserListViewModel vm = new(service.Object);
            vm.GetUsers();

            // assert
            Assert.Equal(
                    ((List<User>)users).Count,
                    vm.AllUsers.Count
                );
        }
    
        [Fact]
        public void SelectAnUserShouldRaisePropertyChange()
        {
            // Arrange
            bool invoked = false;
            var userVm = new UserListViewModel(new Mock<IUserService>().Object);

            userVm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("SelectedUser"))
                    invoked = true;
            };

            // Act
            userVm.SelectedUser = new User();

            // Assert
            Assert.True(invoked);
        }

        [Fact]
        public void SelectedUserShouldRaisePropertyChange()
        {
            // Arrange
            bool invoked = false;
            var userVm = new UserListViewModel(new Mock<IUserService>().Object);

            userVm.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsSelected"))
                    invoked = true;
            };

            // Act
            userVm.IsSelected = true;

            // Assert
            Assert.True(invoked);
        }
    }
}