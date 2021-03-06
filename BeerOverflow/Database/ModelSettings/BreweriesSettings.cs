﻿using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    class BreweriesSettings: IEntityTypeConfiguration<Brewery>
    {
        public void Configure(EntityTypeBuilder<Brewery> builder)
        {
            builder.HasKey(c => c.ID);
            builder.Property(p => p.Name).IsRequired();
            builder.HasOne(c => c.Country).WithMany(b => b.Breweries).HasForeignKey(k => k.CountryID);
            builder.Property(p => p.CountryID).IsRequired();
            builder.Property(p => p.CreatedOn).HasColumnType("datetime2").IsRequired();
            builder.Property(p => p.ModifiedOn).HasColumnType("datetime2");
            builder.Property(p => p.DeletedOn).HasColumnType("datetime2");
            builder.Property(p => p.IsDeleted).IsRequired().HasDefaultValue(false);
        }
    }
}
