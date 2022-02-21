using ETL.Project.Analysis.Models;
using ETL.Project.Database;
using ETL.Project.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ETL.Project
{
    public class Main
    {
        private readonly IConfiguration _config;

        public Main(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void Start()
        {
            var context = Program.Provider.GetService<ClientDBContext>();
            context.Database.Migrate();

            if (_config.GetValue<bool>("GenerateIntialData"))
            {
                DataGenerator.GenerateAll(context, 100);
                DataGenerator.GenerateUsers(context, 10);
            }

            var analysisContext = Program.Provider.GetService<AnalystDBContext>();
            analysisContext.Database.Migrate();

            PopulateAnalysisContext(context, analysisContext);
            AddUsersWithoutLibrary(context, analysisContext);

            Environment.Exit(0);
        }

        public static void PopulateAnalysisContext(
            ClientDBContext originContext, AnalystDBContext analysisContext)
        {
            var libraries = originContext.Library.Select(lib => new LibResponse
            {
                UserId = lib.User.Id,
                Name = lib.Game.Name,
                Genre = lib.Game.Genre.Name,
                Year = lib.Game.Year,
                AquisitionDate = lib.AquisitionDate
            }).ToList();

            foreach (LibResponse res in libraries)
            {
                var user = originContext.Address
                    .Where(u => u.User.Id == res.UserId)
                    .Select(address => new User
                    {
                        Name = address.User.Name,
                        Birthday = address.User.Birthday,
                        Address = address.Endereco,
                        City = address.Cidade,
                        State = address.Estado,
                        Complement = address.Complemento
                    }).First();

                Library library = new()
                {
                    User = user,
                    Name = res.Name,
                    Genre = res.Genre,
                    Year = res.Year,
                    AquisitionDate = res.AquisitionDate
                };

                analysisContext.Library.Add(library);
            }
            analysisContext.SaveChanges();
        }

        public static void AddUsersWithoutLibrary(
            ClientDBContext originContext, AnalystDBContext analysisContext)
        {
            var usersWithoutLibrary =
                from user in originContext.User.ToList()
                join library in originContext.Library.ToList()
                    on user equals library.User into gj
                join address in originContext.Address.ToList()
                    on user equals address.User into gj2
                from subuser in gj.DefaultIfEmpty()
                from subAddress in gj2.DefaultIfEmpty()
                    where user.Libraries == null
                    select new User {
                            Name = user.Name,
                            Birthday = user.Birthday,
                            Address = subAddress?.Endereco ?? "",
                            City = subAddress?.Cidade ?? "",
                            State = subAddress?.Estado ?? "",
                            Complement = subAddress?.Complemento ?? ""
                    };

            foreach (User user in usersWithoutLibrary.ToList())
            {
                analysisContext.User.Add(user);
            }
            analysisContext.SaveChanges();
        }
    }
}
