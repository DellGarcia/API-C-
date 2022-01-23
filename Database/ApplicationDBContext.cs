using Microsoft.EntityFrameworkCore;
using Api_CSharp.Models;
using System;

namespace Api_CSharp.Database 
{
  public class ApplicationDBContext : DbContext
  {
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {

    }
    public DbSet<Api_CSharp.Models.User> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.CreationDate)
            .HasDefaultValue(DateTime.Now);
    }
  }
}