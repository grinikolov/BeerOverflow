using BeerOverflow.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Seeder
{
    public class BOSeeder
    {
        public static async System.Threading.Tasks.Task InitAsync(BOContext context)
        {
            await SeedRolesAsync(context);
            await SeedCountriesAsync(context);
        }
        private static async System.Threading.Tasks.Task SeedRolesAsync(BOContext context)
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

        private static async System.Threading.Tasks.Task SeedCountriesAsync(BOContext context)
        {
            if (context.Countries.Any())
                return;

            var countryNames = new[] { "Bulgaria", "Germany", "Chech Republic" };
            await context.Countries.AddRangeAsync(
                countryNames.Select(name => new Country()
                {
                    Name = name,
                })
            );
            await context.SaveChangesAsync();
        }

        //private static async System.Threading.Tasks.Task SeedBreweriesAsync(BOContext context)
        //{
        //    if (context.Breweries.Any())
        //        return;

        //    var breweryNames = new[] { "Bulgaria", "Germany", "Chech Republic" };
        //    await context.Countries.AddRangeAsync(
        //        breweryNames.Select(name => new Brewery()
        //        {
        //            Name = name,
        //            c
        //        })
        //    );
        //    await context.SaveChangesAsync();
        //}

    }
}
