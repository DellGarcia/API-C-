using Microsoft.EntityFrameworkCore;
using ETL.Project.Mappings;
using ETL.Project.Models;
using System;
using Microsoft.Extensions.Configuration;

namespace ETL.Project.Database
{
    public class ClientDBContext : DbContext
    {
        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Library> Library { get; set; }

        public ClientDBContext(DbContextOptions<ClientDBContext> options) : base(options)
        {}

        public ClientDBContext()
        {}

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