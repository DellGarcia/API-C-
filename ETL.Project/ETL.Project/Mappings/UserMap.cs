using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ETL.Project.Models;

namespace ETL.Project.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.HasOne<Address>()
                .WithMany()
                .HasForeignKey(p => p.AddressId)
                .IsRequired();
        }
    }
}
