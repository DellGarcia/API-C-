using ETL.Project.Database;
using Microsoft.EntityFrameworkCore;
using ETL.Project.Utils;
using System.Text.Json;
using System;
using ETL.Project.Analysis.Models;
using System.Collections.Generic;

namespace ETL.Project
{
    internal class Program
    {
        public static void Main()
        {
            var context = new ClientDBContext();
            context.Database.Migrate();

            //DataGenerator.Generate(context);

            //ExtractAndPopulateUsers(context);
            ExtractAndPopulate(context);
        }

        public static async void ExtractAndPopulateUsers(ClientDBContext context)
        {
            var responseAddress = await context.Address
                .Include(address => address.User)
                .ToListAsync();

            var analysisContext = new AnalystDBContext();
            analysisContext.Database.Migrate();

            // var response2 = await context.User.ToListAsync();

            User user;
            foreach (var item in responseAddress)
            {
                user = new()
                {
                    Name = item.User.Name,
                    Birthday = item.User.Birthday,
                    Address = item.Endereco,
                    City = item.Cidade,
                    State = item.Estado,
                    Complement = item.Complemento
                };
                analysisContext.User.Add(user);
            }
            analysisContext.SaveChanges();
        }

        public static async void ExtractAndPopulate(ClientDBContext context)
        {
            var response = await context.Library
                .Include(library => library.Game)
                .Include(library => library.User)
                .ThenInclude(user => user.Addresses)
                .ToListAsync();

            //var response = await context.Library.ToListAsync();

            var analysisContext = new AnalystDBContext();
            analysisContext.Database.Migrate();

            Library library;
            foreach (var item in response)
            {
                //Models.Address address = new List<Models.Address>(item.User.Addresses)[0];

                //User user = new()
                //{
                //    Name = item.User.Name,
                //    Birthday = item.User.Birthday,
                //    Address = address.Endereco,
                //    City = address.Cidade,
                //    State = address.Estado,
                //    Complement = address.Complemento
                //};

                //library = new()
                //{
                //    User = user,
                //    Name = item.Game.Name,
                //    Genre = item.Game.Genre.Name,
                //    Year = item.Game.Year,
                //    AquisitionDate = item.AquisitionDate
                //};
                //analysisContext.Library.Add(library);
            }
            analysisContext.SaveChanges();
        }
    }
}
