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
using System.Net.Http;
using System.Net;

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
            Assert.IsType<NotFoundObjectResult>(result);
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
        public void GetUser_WithoutIdInRoute_ReturnsListOfAllUsers()
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

            dbSet.Setup(c => c.ToList()).Returns(users);
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = controller.GetUser().Value;

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
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
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
            Assert.IsType<BadRequestObjectResult>(actionResult.Result);
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
            Assert.IsType<NotFoundObjectResult>(value);
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
            Guid uid = Guid.NewGuid();

            User previousUserData = new()
            {
                Id = uid,
                FirstName = "Lucasss",
                SurName = "",
                Age = 20
            };

            User newUserData = new()
            {
                FirstName = "Lucas",
                SurName = "Garcia",
                Age = 20
            };

            dbSet.Object.Add(previousUserData);
            context.Setup(c => c.User).Returns(dbSet.Object);
            await context.Object.SaveChangesAsync();

            context.Setup(c => c.Update(previousUserData));

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = await controller.PutUser(uid, newUserData);

            // assert
            Assert.IsType<NoContentResult>(value);
        }


        [Fact]
        public async void PutUser_UserIsNotValid_ReturnsBadRequest()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();
            Guid uid = Guid.NewGuid();

            User invalidUser = new()
            {
                Id = uid,
                FirstName = "Lucas",
                SurName = "Garcia",
                Age = 5
            };

            dbSet.Object.Add(invalidUser);
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);

            // act
            var value = await controller.PutUser(uid, invalidUser);

            // assert
            Assert.IsType<BadRequestObjectResult>(value);
        }

        [Fact]
        public async void PutUser_UserNotExist_ReturnsNotFound()
        {
            // arrange
            var logger = new Mock<ILogger<UserController>>();
            var context = new Mock<ApplicationDBContext>();
            var dbSet = new Mock<DbSet<User>>();
            Guid uid = Guid.NewGuid();

            User newUserData = new()
            {
                Id = uid,
                FirstName = "Lucas",
                SurName = "Garcia",
                Age = 20
            };

            //dbSet.Setup(c => c.Any(e => newUserData.Id == uid)).Returns(false);
            context.Setup(c => c.User).Returns(dbSet.Object);

            var controller = new UserController(logger.Object, context.Object);
            // act
            var value = await controller.PutUser(uid, newUserData);

            // assert
            Assert.IsType<NotFoundObjectResult>(value);
        }

        #endregion
    }
}