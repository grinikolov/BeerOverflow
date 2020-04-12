using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    class CommentSettings : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(p => p.UserID).IsRequired();
            builder.Property(p => p.BeerID).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.LikesCount).IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnType("datetime2").IsRequired();
            builder.Property(p => p.ModifiedOn).HasColumnType("datetime2");
            builder.Property(p => p.DeletedOn).HasColumnType("datetime2");
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
