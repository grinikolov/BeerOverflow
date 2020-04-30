using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    class UserSettings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasMany(c => c.CommentList).WithOne(c => c.User).OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnType("datetime2").IsRequired();
            builder.Property(p => p.ModifiedOn).HasColumnType("datetime2");
            builder.Property(p => p.DeletedOn).HasColumnType("datetime2");
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.HasOne(r => r.Role).WithMany(r => r.Users).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
