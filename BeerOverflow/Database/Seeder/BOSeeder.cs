using BeerOverflow.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Seeder
{
    public class BOSeeder
    {
        public static async Task InitAsync(BOContext context)
        {
            await SeedRolesAsync(context);
            await SeedCountriesAsync(context);
            await SeedStylesAsync(context);
            //await SeedFirstAdmin(context);
            await SeedBreweriesAsync(context);
            await SeedBeersAsync(context);

        }
        private static async Task SeedRolesAsync(BOContext context)
        {
            if (context.Roles.Any())
                return;

            var roleNames = new[] { "member", "admin" };
            await context.Roles.AddRangeAsync(
                roleNames.Select(name => new Role()
                {
                    Name = name,
                    NormalizedName = name.ToUpper(),

                })
            );
            await context.SaveChangesAsync();
        }

        private static async Task SeedStylesAsync(BOContext context)
        {
            if (context.BeerStyles.Any())
                return;

            await context.BeerStyles.AddAsync(new BeerStyle()
            {
                Name = "Lager",
                Description = "Pale lagers are the standard international beer style, as personified by products from Miller to Heineken. This style is the generic spin-off of the pilsner style. Pale lagers are generally light- to medium-bodied with a light-to-medium hop impression and a clean, crisp malt character.",
                CreatedOn = DateTime.UtcNow,
                   
            });
            await context.SaveChangesAsync();
        }

        private static async Task SeedCountriesAsync(BOContext context)
        {
            if (context.Countries.Any())
                return;

            var countryNames = new[] { "Bulgaria", "Germany", "Chech Republic" };
            await context.Countries.AddRangeAsync(
                countryNames.Select(name => new Country()
                {
                    Name = name,
                    CreatedOn = DateTime.UtcNow,
                })
            );
            await context.SaveChangesAsync();
        }

        //private static async Task SeedFirstAdmin(BOContext context)
        //{
        //    if (context.Users.Any())
        //    {
        //        return;
        //    }

        //    var user = new User()
        //    {
        //        Name = "Carlsberg",
        //        UserName = "Carlsberg",
        //        NormalizedUserName = "CARLSBERG",
        //        Email = "Carlsberg@bo.com",
        //        NormalizedEmail = "CARSLBERG@BO.COM",
        //        Password = "carlsberg",
        //        Role = await context.Roles.FindAsync(2)

        //    };
        //    await context.Users.AddAsync(user);
        //    await context.SaveChangesAsync();
        //}

        private static async Task SeedBreweriesAsync(BOContext context)
        {
            if (context.Breweries.Any())
                return;

            await context.Breweries.AddAsync(
                new Brewery()
                {
                    Name = "Carlsberg",
                    CountryID = context.Countries.FirstOrDefault(c => c.Name == "Bulgaria").ID,
                    Country = context.Countries.FirstOrDefault(c => c.Name == "Bulgaria"),
                    CreatedOn = DateTime.UtcNow
                });
            await context.SaveChangesAsync();
        }

        private static async Task SeedBeersAsync(BOContext context)
        {
            if (context.Beers.Any())
                return;

            var beerNames = new[] { "Carlsberg", "Shumensko", "Pirinsko" };
            foreach (var beer in beerNames)
            {
                var b = new Beer() 
                {
                    Name = beer,
                    Country = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria"),
                    Brewery = await context.Breweries.FirstOrDefaultAsync(b => b.Name == "Shumensko"),
                    ABV = 4,
                    Style = await context.BeerStyles.FindAsync(1),
                    CreatedOn = DateTime.UtcNow
                };
                b.CountryID = b.Country.ID;
                b.BreweryID = b.Brewery.ID;
                b.StyleID = b.Style.ID;
                await context.Beers.AddAsync(b);
            }
            await context.SaveChangesAsync();
        }
    }
}
