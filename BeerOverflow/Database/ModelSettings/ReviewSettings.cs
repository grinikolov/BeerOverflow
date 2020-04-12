using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    public class ReviewSettings : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(r => r.ID);
            builder.HasIndex(r => new {r.BeerID, r.UserID }).IsUnique();
            builder.HasOne(c => c.User).WithMany(r => r.ReviewList).OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.Rating).IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnType("datetime2").IsRequired();
            builder.Property(p => p.ModifiedOn).HasColumnType("datetime2");
            builder.Property(p => p.DeletedOn).HasColumnType("datetime2");
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(p => p.IsFlagged).IsRequired().HasDefaultValue(false);
        }
    }
}
