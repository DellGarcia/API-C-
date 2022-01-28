using Api_CSharp.Database;
using Api_CSharp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace api_csharp.Repositories
{
    public class UserRepository
    {
        protected readonly ApplicationDBContext context;
        protected readonly DbSet<User> dbSet;

        public UserRepository(ApplicationDBContext context)
        {
            this.context = context;

            dbSet = context.Set<User>();
        }

        public virtual async Task<User> Create(User entity)
        {
            var data = await dbSet.AddAsync(entity);
            await SaveChanges();

            return data.Entity;
        }

        public virtual async Task<bool> DeleteById(Guid id)
        {
            User entity = await dbSet
                .FirstOrDefaultAsync(e => e.Id == id);

            dbSet.Remove(entity);
            await SaveChanges();

            return true;
        }

        public virtual async Task<User> Update(User entity)
        {
            var entry = dbSet.Update(entity);
            await SaveChanges();

            return entry.Entity;
        }

        public virtual async Task<User> GetById(Guid id)
        {
            return await dbSet
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task<int> SaveChanges()
        {
            return await context.SaveChangesAsync();
        }
    }
}