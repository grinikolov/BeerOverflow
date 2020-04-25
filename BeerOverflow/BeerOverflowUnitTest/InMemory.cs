using Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflowUnitTest
{
    public class InMemory
    {
        public static DbContextOptions<BOContext> GetOptions(string databaseName)
        {
            return new DbContextOptionsBuilder<BOContext>().UseInMemoryDatabase(databaseName).Options;
        }
    }
}
