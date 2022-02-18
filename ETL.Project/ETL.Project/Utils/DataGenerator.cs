using ETL.Project.Database;
using ETL.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Utils
{
    public class DataGenerator
    {
        public static void Generate(ClientDBContext context)
        {

            for (int i = 1; i <= 100; i++)
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
    }
}
