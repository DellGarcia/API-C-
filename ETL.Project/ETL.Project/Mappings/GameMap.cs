using ETL.Project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETL.Project.Mappings
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasOne<Genre>()
                .WithMany()
                .HasForeignKey(p => p.GenreId)
                .IsRequired();
        }
    }
}
