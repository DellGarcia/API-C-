using System;
using ETL.Project.Database;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ETL.Project.Utils;
namespace ETL.Project
{
    internal class Program
    {
        public static void Main()
        {
            var context = new ClientDBContext();
            context.Database.Migrate();

            AddressGenerator.Generate(context);
            UserGenerator.Generate(context);
            GenreGenerator.Generate(context);
            GameGenerator.Generate(context);
            LibraryGenerator.Generate(context);
        }
    }
}
