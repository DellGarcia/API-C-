using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Api_CSharp.Controllers;
using System;
using System.Threading.Tasks;
using Api_CSharp.Database;
using Microsoft.AspNetCore.Mvc;
using Api_CSharp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace api_csharp.tests
{
    public class UserControllerTest
    {

        #region Get Tests
        
        [Fact]
        public async void GetUser_UserNotExists_ReturnsNotFound()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();

            dbSet.Setup(db => db.FindAsync(It.IsAny<Guid>())).Returns(null);
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var result = (await controller.GetUser(Guid.NewGuid())).Result;

            // assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetUser_UserExists_ReturnsUser()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();
            var user = new User();

            var valueTask = new ValueTask<User>(
                Task<User>.Factory.StartNew(
                    () => user)
                );

            dbSet.Setup(db => db.FindAsync(It.IsAny<Guid>())).Returns(valueTask);
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = (await controller.GetUser(Guid.NewGuid())).Value;

            // assert
            Assert.IsType<User>(value);
        }

        [Fact]
        public async void GetUser_WithoutIdInRoute_ReturnsListOfAllUsers()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();

            List<User> users = new()
            {
                new User(),
                new User(),
                new User()
            };

            users.ForEach(u => dbSet.Object.Add(u));

            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = (await controller.GetUser()).Value;

            // assert
            Assert.IsAssignableFrom<IEnumerable<User>>(value);

            List<User> result = Enumerable.ToList(value);

            Assert.Equal(result.Count, users.Count);
        }

        #endregion

        #region Post Tests

        [Fact]
        public async void PostUser_UserIsValid_ReturnsRegisteredUser()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();

            var user = new User()
            {
                FirstName = "Nome",
                SurName = "Sobrenome",
                Age = 20
            };

            dbSet.Setup(db => db.Add(user));
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var actionResult = await controller.PostUser(user);
            var registeredUser = (actionResult.Result as CreatedAtActionResult).Value;

            // assert
            Assert.IsType<User>(registeredUser);
            if (registeredUser.GetType() == typeof(User))
            {
                Assert.IsType<Guid>(((User)registeredUser).Id);
            }
        }

        [Fact]
        public async void PostUser_UserFirstNameIsNotFilled_ReturnsBadRequest()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();

            var user = new User()
            {
                FirstName = null,
                SurName = null,
                Age = 20
            };

            dbSet.Setup(db => db.Add(user));
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var actionResult = await controller.PostUser(user);

            // assert
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        [Fact]
        public async void PostUser_UserAgeIsInvalid_ReturnsBadRequest()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();

            var user = new User()
            {
                FirstName = "Nome",
                SurName = "Sobrenome",
                Age = -10
            };

            dbSet.Setup(db => db.Add(user));
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var actionResult = await controller.PostUser(user);

            // assert
            Assert.IsType<BadRequestResult>(actionResult.Result);
        }

        #endregion

        #region Delete Tests

        [Fact]
        public async void DeleteUser_UserExists_ReturnsNoContent()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();
            var user = new User();

            var valueTask = new ValueTask<User>(
                Task<User>.Factory.StartNew(
                    () => user)
                );

            dbSet.Setup(db => db.FindAsync(It.IsAny<Guid>())).Returns(valueTask);
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = await controller.DeleteUser(Guid.NewGuid());

            // assert
            Assert.IsType<NoContentResult>(value);
        }

        [Fact]
        public async void DeleteUser_UserNotExists_ReturnsNoFound()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();

            dbSet.Setup(db => db.FindAsync(It.IsAny<Guid>())).Returns(null);
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = await controller.DeleteUser(Guid.NewGuid());

            // assert
            Assert.IsType<NotFoundResult>(value);
        }

        #endregion

        #region Update Testes

        [Fact]
        public async void PutUser_UserIsValid_ReturnsNoContent()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();
            Guid uid = It.IsAny<Guid>();

            User previousUserData = new()
            {
                Id = uid,
                FirstName = "Lucasss",
                SurName = null,
                Age = 20
            };

            User newUserData = new()
            {
                Id = uid,
                FirstName = "Lucas",
                SurName = "Garcia",
                Age = 20
            };

            context.Setup(c => c.User).Returns(dbSet.Object);
            context.Object.Entry(previousUserData).State = EntityState.Added;
            context.Object.SaveChanges();

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = await controller.PutUser(uid, newUserData);

            // assert
            Assert.IsType<NoContentResult>(value);
        }

        #endregion
    }
}