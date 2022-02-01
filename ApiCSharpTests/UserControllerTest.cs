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
    public class UserControllerTest : IDisposable
    {
        private readonly ApplicationDBContext _context;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase("apisharp-db")
                .EnableSensitiveDataLogging();

            _context = new ApplicationDBContext(dbContextOptions.Options);
            _context.Database.EnsureCreated();

            Mock<ILogger<UserController>> logger = new Mock<ILogger<UserController>>();
            _controller = new UserController(logger.Object, _context);
        }

        public void Dispose()
        {
            try
            {
                _context.User.RemoveRange(_context.User);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }
            _context.Dispose();
        }

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
        public async void GetUser_WithoutIdInRoute_ReturnsListOfAllUsers()
        {
            // arrange
            List<User> users = new()
            {
                new User() { Id = Guid.NewGuid(), FirstName = "Lucas", Age = 19 },
                new User() { Id = Guid.NewGuid(), FirstName = "Felipe", Age= 25 },
                new User() { Id = Guid.NewGuid(), FirstName = "Guilherme", Age = 35 }
            };

            users.ForEach(u => _context.User.Add(u));
            await _context.SaveChangesAsync();

            // act
            var value = _controller.GetUser().Value;

            // assert
            Assert.IsAssignableFrom<IEnumerable<User>>(value);

            List<User> result = Enumerable.ToList(value);

            Assert.Equal(users.Count, result.Count);
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
            Guid uid = Guid.NewGuid();

            User previousUserData = new()
            {
                Id = uid,
                FirstName = "Lucasss",
                SurName = "",
                Age = 20
            };

            User invalidUserUpdate = new()
            {
                FirstName = "Lucas",
                SurName = "Garcia",
                Age = 15
            };

            _context.User.Add(previousUserData);
            _context.SaveChanges();

            // act
            var value = await _controller.PutUser(uid, invalidUserUpdate);

            // assert
            Assert.IsType<NoContentResult>(value);
        }


        [Fact]
        public async void PutUser_UserIsNotValid_ReturnsBadRequest()
        {
            // arrange
            Guid uid = Guid.NewGuid();

            User previousUserData = new()
            {
                Id = uid,
                FirstName = "Lucasss",
                SurName = "",
                Age = 20
            };

            User invalidUserUpdate = new()
            {
                FirstName = "Lucas",
                SurName = "Garcia",
                Age = 5
            };

            _context.User.Add(previousUserData);
            _context.SaveChanges();

            // act
            var value = await _controller.PutUser(uid, invalidUserUpdate);

            // assert
            Assert.IsType<BadRequestObjectResult>(value);
        }

        [Fact]
        public async void PutUser_UserNotExist_ReturnsNotFound()
        {
            // arrange
            Guid uid = Guid.NewGuid();

            User newUserData = new()
            {
                Id = uid,
                FirstName = "Lucas",
                SurName = "Garcia",
                Age = 20
            };

            // act
            var value = await _controller.PutUser(uid, newUserData);

            // assert
            Assert.IsType<NotFoundObjectResult>(value);
        }

        #endregion
    }
}