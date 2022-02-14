using Xunit;
using Moq;
using AwesomeApp.Services;
using AwesomeApp.Models;
using AwesomeApp.ViewModels;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

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
    }
}