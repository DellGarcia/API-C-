using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Api_CSharp.Models;

namespace api_csharp.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder.Property(user => user.CreationDate)
                .HasDefaultValue(DateTime.Now);

            builder.Property(user => user.FirstName)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(user => user.SurName)
                .HasMaxLength(150);

            builder.Property(user => user.Age)
                .IsRequired();
        }
    }
}