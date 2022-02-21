using Microsoft.EntityFrameworkCore;
using ETL.Project.Analysis.Mappings;
using ETL.Project.Analysis.Models;
using System;
using Microsoft.Extensions.Configuration;

namespace ETL.Project.Database
{
    public class AnalystDBContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Library> Library { get; set; }

        public AnalystDBContext(DbContextOptions<AnalystDBContext> options) : base(options)
        {}

        public AnalystDBContext()
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new LibraryMap());
        }
    }
}