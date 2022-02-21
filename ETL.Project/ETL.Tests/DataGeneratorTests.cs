using System;
using Xunit;
using Moq;
using ETL.Project.Database;
using ETL.Project.Models;
using ETL.Project.Utils;
using Microsoft.EntityFrameworkCore;

namespace ETL.Tests
{
    public class DataGeneratorTests
    {
        [Fact]
        public void GenerateAll_GenerateCorrectNumberOfValues()
        {
            // arrange
            Mock<ClientDBContext> context = new();
            Mock<DbSet<Library>> libs = new();
            Mock<DbSet<Address>> addresses = new();

            context.Setup(c => c.Library).Returns(libs.Object);
            context.Setup(c => c.Address).Returns(addresses.Object);

            Random random = new();
            int amount = random.Next(100);

            // act
            DataGenerator.GenerateAll(context.Object, amount);

            // assert
            libs.Verify(libs => libs.Add(It.IsAny<Library>()), Times.Exactly(amount));
            addresses.Verify(addresses => addresses.Add(It.IsAny<Address>()), Times.Exactly(amount));
            context.Verify(c => c.SaveChanges(), Times.Once());
        }
    
        [Fact]
        public void GenerateUsers_GenerateCorrectNuberOfUsers()
        {
            // arrange
            Mock<ClientDBContext> context = new();
            Mock<DbSet<User>> users = new();

            context.Setup(c => c.User).Returns(users.Object);

            Random random = new();
            int amount = random.Next(100);

            // act
            DataGenerator.GenerateUsers(context.Object, amount);

            // assert
            users.Verify(u => u.Add(It.IsAny<User>()), Times.Exactly(amount));
            context.Verify(c => c.SaveChanges(), Times.Once());
        }
    }
}
