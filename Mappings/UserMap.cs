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
                .HasColumnType("varchar(150)");

            builder.Property(user => user.SurName)
                .HasColumnType("varchar(150)");
        }
    }
}