using Microsoft.EntityFrameworkCore;
using ETL.Project.Mappings;
using ETL.Project.Models;
using System;

namespace ETL.Project.Database
{
    public class ClientDBContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Library> Library { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = "server=localhost;port=3306;database=clientETL-database;uid=root;password=";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AddressMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new GenreMap());
            modelBuilder.ApplyConfiguration(new GameMap());
            modelBuilder.ApplyConfiguration(new LibraryMap());
        }
    }
}