using ETL.Project.Database;
using ETL.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Utils
{
    public class GameGenerator
    {
        public static async void Generate(ClientDBContext context)
        {
            Game game;
            for (int i = 1; i <= 100; i++)
            {
                game = new Game()
                {
                    Name = $"game {i}",
                    Year = 1920 + i,
                    GenreId = i
                };
                await context.Game.AddAsync(game);
            }
            context.SaveChanges();
        }
    }
}
