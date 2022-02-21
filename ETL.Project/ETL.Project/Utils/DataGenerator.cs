using ETL.Project.Database;
using ETL.Project.Models;
using System;

namespace ETL.Project.Utils
{
    public class DataGenerator
    {
        public static void GenerateAll(ClientDBContext context, int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                Genre genre = new()
                {
                    Name = $"Genre {i}",
                    Description = $"Description {i}"
                };

                Game game = new()
                {
                    Name = $"Game {i}",
                    Year = 1920 + i,
                    Genre = genre
                };

                User user = new() {
                    Name = $"user {i}",
                    Birthday = DateTime.Now
                };

                Address address = new()
                {
                    Endereco = $"Rua {i}",
                    Complemento = $"complemento {i}",
                    Cidade = $"cidade {i}",
                    Estado = $"estado {i}",
                    User = user,
                };

                Library library = new()
                {
                    AquisitionDate = DateTime.Now,
                    Game = game,
                    User = user,
                };
                context.Library.Add(library);
                context.Address.Add(address);
            }
            context.SaveChanges();
        }
    
        public static void GenerateUsers(ClientDBContext context, int amount)
        {
            for(int i = 1; i <= amount; i++)
            {
                User user = new()
                {
                    Name = $"Sigle User {i}",
                    Birthday = DateTime.Now
                };

                context.User.Add(user);
            }
            context.SaveChanges();
        }
    }
}
