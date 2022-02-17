using ETL.Project.Database;
using ETL.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Utils
{
    public class GenreGenerator
    {
        public static async void Generate(ClientDBContext context)
        {
            Genre genre;
            for (int i = 1; i <= 100; i++)
            {
                genre = new Genre()
                {
                    Name = $"genero {i}",
                    Description = $"descrition {i}"
                };
                await context.Genre.AddAsync(genre);
            }
            context.SaveChanges();
        }
    }
}
