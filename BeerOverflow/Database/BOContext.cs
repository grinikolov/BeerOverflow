﻿using BeerOverflow.Models;
using Database.ModelSettings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Database
{
    public class BOContext : DbContext// IdentityDbContext<User, Role, Guid>
    {

        public static readonly ILoggerFactory MyLoggerFactory
= LoggerFactory.Create(builder =>
{
    builder
        .AddFilter((category, level) =>
            category == DbLoggerCategory.Database.Command.Name
            && level == LogLevel.Information)
        .AddConsole();
});

        public BOContext(DbContextOptions<BOContext> options)
            : base(options)
        {
        }
        public DbSet<Beer> Beers { get; set; }
        public DbSet<BeerStyle> BeerStyles { get; set; }
        public DbSet<Brewery> Breweries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<DrankList> DrankLists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CountriesSetting());
            builder.ApplyConfiguration(new BreweriesSettings());
            builder.ApplyConfiguration(new BeerStyleSettings());
            builder.ApplyConfiguration(new UserSettings());
            builder.ApplyConfiguration(new CommentSettings());
            builder.ApplyConfiguration(new ReviewSettings());
            builder.ApplyConfiguration(new BeerSettings());
            builder.ApplyConfiguration(new LikeSettings());
            builder.ApplyConfiguration(new FlagSettings());
            builder.ApplyConfiguration(new DrankListSettings());
            builder.ApplyConfiguration(new WishListSettings());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time
        }

    }
}
