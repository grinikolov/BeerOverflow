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


    }
}
