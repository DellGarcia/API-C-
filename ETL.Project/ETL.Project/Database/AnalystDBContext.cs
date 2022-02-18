using Microsoft.EntityFrameworkCore;
using ETL.Project.Analysis.Mappings;
using ETL.Project.Analysis.Models;
using System;

namespace ETL.Project.Database
{
    public class AnalystDBContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Library> Library { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;port=3306;database=ETL-database;uid=root;password=";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new LibraryMap());
        }
    }
}