using BeerOverflow.Models;
using Database.ModelSettings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace Database
{
    public class BOContext :  IdentityDbContext<User, Role, int>
    {

        //public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        //        {           
        //            builder
        //                .AddFilter((category, level) =>
        //                    category == DbLoggerCategory.Database.Command.Name
        //                    && level == LogLevel.Information)
        //                .AddConsole();
        //        });

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
        //public DbSet<User> Users { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<BeerUserRating> BeerUserRatings { get; set; }
        //public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "member",
                    NormalizedName = "MEMBER",
                },
                new Role
                {
                    Id = 2,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                });

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
            builder.ApplyConfiguration(new BeerUserRatingSettings());
            
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { 
            //optionsBuilder
            //    .UseLoggerFactory(MyLoggerFactory); // Warning: Do not create a new ILoggerFactory instance each time
            base.OnConfiguring(optionsBuilder);
        }

    }
}
