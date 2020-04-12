using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    public class BeerSettings : IEntityTypeConfiguration<Beer>
    {
        public void Configure(EntityTypeBuilder<Beer> builder)
        {
            builder.HasKey(k => k.ID);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.ABV).IsRequired();
            builder.Property(p => p.CountryID).IsRequired();
            builder.Property(p => p.BreweryID).IsRequired();
            builder.Property(p => p.StyleID).IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnType("datetime2").IsRequired();
            builder.Property(p => p.ModifiedOn).HasColumnType("datetime2");
            builder.Property(p => p.DeletedOn).HasColumnType("datetime2");
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
