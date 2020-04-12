using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    public class FlagSettings : IEntityTypeConfiguration<Flag>
    {
        public void Configure(EntityTypeBuilder<Flag> builder)
        {
            builder.HasKey(k => new { k.UserID, k.ReviewID });
        }
    }
}
