using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETL.Project.Database;
using ETL.Project.Models;

namespace ETL.Project.DAO
{
    public class UserDao
    {
        private readonly ClientDBContext context;

        public UserDao(ClientDBContext context)
        {
            this.context = context;
        }

        public async void Add(User user)
        {
            await context.User.AddAsync(user);
        }

        public IEnumerable<User> GetAll()
        {
            return context.User.ToList(); 
        }
    }
}
