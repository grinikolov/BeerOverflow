using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    class BeerUserRatingSettings : IEntityTypeConfiguration<BeerUserRating>
    {
        public void Configure(EntityTypeBuilder<BeerUserRating> builder)
        {
            builder.HasKey(k => new { k.UserID, k.BeerID});
        }
    }
}
