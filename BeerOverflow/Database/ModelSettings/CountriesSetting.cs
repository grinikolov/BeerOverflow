using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    public class CountriesSetting : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(c => c.ID);
            builder.HasIndex(c => c.Name).IsUnique();
            builder.HasMany(b => b.Breweries).WithOne(c => c.Country).OnDelete(DeleteBehavior.Restrict);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnType("datetime2").IsRequired();
            builder.Property(p => p.ModifiedOn).HasColumnType("datetime2");
            builder.Property(p => p.DeletedOn).HasColumnType("datetime2");
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
