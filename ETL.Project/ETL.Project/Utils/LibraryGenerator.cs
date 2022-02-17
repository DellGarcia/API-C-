using ETL.Project.Database;
using ETL.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Utils
{
    public class LibraryGenerator
    {
        public static async void Generate(ClientDBContext context)
        {
            Library library;
            for (int i = 1; i <= 100; i++)
            {
                library = new Library()
                {
                    AquisitionDate = DateTime.Now,
                    GameId = i,
                    UserId = i
                };
                await context.Library.AddAsync(library);
            }
            context.SaveChanges();
        }
    }
}
