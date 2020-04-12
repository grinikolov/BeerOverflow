using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.ModelSettings
{
    public class DrankListSettings : IEntityTypeConfiguration<DrankList>
    {
        public void Configure(EntityTypeBuilder<DrankList> builder)
        {
            builder.HasKey(dl => new { dl.BeerID, dl.UserID });
        }
    }
}
