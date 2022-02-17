using ETL.Project.Database;
using ETL.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETL.Project.Utils
{
    public class UserGenerator
    {
        public static async void Generate(ClientDBContext context)
        {
            User user;
            for (int i = 1; i <= 100; i++)
            {
                user = new User()
                {
                    Name = $"user {i}",
                    Birthday = DateTime.Now,
                    AddressId = i,
                };
                await context.User.AddAsync(user);
            }
            context.SaveChanges();
        }
    }
}
