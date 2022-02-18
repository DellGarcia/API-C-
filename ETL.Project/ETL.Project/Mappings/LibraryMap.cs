using ETL.Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETL.Project.Mappings
{
    public class LibraryMap : IEntityTypeConfiguration<Library>
    {
        public void Configure(EntityTypeBuilder<Library> builder)
        {
            builder.HasOne(library => library.User)
                .WithMany(game => game.Libraries)
                .IsRequired();

            builder.HasOne(library => library.Game)
                .WithMany(game => game.Libraries)
                .IsRequired();
        }
    }
}
