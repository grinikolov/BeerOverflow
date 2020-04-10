using BeerOverflow.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Database
{
    public class BOContext : DbContext// IdentityDbContext<User, Role, Guid>
    {
        //public BOContext()
        //{
        //}
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Seeder();
            base.OnModelCreating(builder);
            builder.Entity<Review>()
                .HasKey(r => new { r.ID, r.BeerID, r.UserID });
            builder.Entity<DrankList>()
                   .HasKey(dl => new { dl.BeerID, dl.UserID });
            builder.Entity<WishList>()
                    .HasKey(wl => new { wl.BeerID, wl.UserID });
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    string connectionString = "Server-.\\SQLEXPRESS;Database=BeerOverflow.Database;Trusted_connection=True";
        //    optionsBuilder.UseSqlServer(connectionString);
        //}

    }
}
