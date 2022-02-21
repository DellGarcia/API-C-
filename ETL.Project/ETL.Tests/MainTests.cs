using Xunit;
using Moq;
using System;
using ETL.Project.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ETL.Project;
using ETL.Project.Utils;
using ETL.Project.Models;
using System.Linq;

namespace ETL.Tests
{
    public class MainTests : IDisposable
    {
        private readonly ClientDBContext _clientContext;
        private readonly AnalystDBContext _analystContext;

        public MainTests()
        {
            Mock<IConfiguration> mock = new();

            var clientDbOptions = new DbContextOptionsBuilder<ClientDBContext>()
                .UseInMemoryDatabase("clientETL-database")
                .EnableSensitiveDataLogging();

            _clientContext = new ClientDBContext(clientDbOptions.Options);
            _clientContext.Database.EnsureCreated();

            var analystDbOptions = new DbContextOptionsBuilder<AnalystDBContext>()
                .UseInMemoryDatabase("analystETL-database")
                .EnableSensitiveDataLogging();

            _analystContext = new AnalystDBContext(analystDbOptions.Options);
            _analystContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            try
            {
                _clientContext.User.RemoveRange(_clientContext.User);
                _clientContext.Library.RemoveRange(_clientContext.Library);
                _clientContext.Address.RemoveRange(_clientContext.Address);
                _clientContext.Game.RemoveRange(_clientContext.Game);
                _clientContext.User.RemoveRange(_clientContext.User);
                _clientContext.SaveChanges();

                _analystContext.User.RemoveRange(_analystContext.User);
                _analystContext.Library.RemoveRange(_analystContext.Library);
                _analystContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException) { }
            _clientContext.Dispose();
            _analystContext.Dispose();
        }
    
        [Fact]
        public void TheDataInClientDBNeedToBeReadedAndSaveInAnalystDB()
        {
            // arrange
            Random random = new();
            int amount = random.Next(1, 100);

            DataGenerator.GenerateAll(_clientContext, amount);
            DataGenerator.GenerateUsers(_clientContext, amount);

            // act
            Main.PopulateAnalysisContext(_clientContext, _analystContext);
            Main.AddUsersWithoutLibrary(_clientContext, _analystContext);

            // assert
            Assert.Equal(amount, _analystContext.Library.ToList().Count);
            Assert.Equal(amount + amount, _analystContext.User.ToList().Count);
        }

        [Fact]
        public void PopulateAnalysisContext_NeedToSaveTheSameAmount()
        {
            // arrange
            Random random = new();
            int amount = random.Next(1, 100);

            DataGenerator.GenerateAll(_clientContext, amount);

            // act
            Main.PopulateAnalysisContext(_clientContext, _analystContext);

            // assert
            Assert.Equal(amount, _analystContext.Library.ToList().Count);
            Assert.Equal(amount, _analystContext.User.ToList().Count);
        }

        [Fact]
        public void AddUsersWithoutLibrary_NeedToSaveTheSameAmount()
        {
            // arrange
            Random random = new();
            int amount = random.Next(1, 100);

            DataGenerator.GenerateUsers(_clientContext, amount);

            // act
            Main.AddUsersWithoutLibrary(_clientContext, _analystContext);

            // assert
            Assert.Equal(amount, _analystContext.User.ToList().Count);
        }
    }
}
