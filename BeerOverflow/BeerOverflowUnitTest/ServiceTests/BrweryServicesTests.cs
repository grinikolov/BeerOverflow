using BeerOverflow.Models;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerOverflowUnitTest.ServiceTests
{
    [TestClass]
    public class BrweryServicesTests
    {
        [TestMethod]
        public async Task GetAllAsync_ShouldReturnEmptyIfNoBreweriesAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnEmptyIfNoBreweriesAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnIEnumerableBreweryyDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnIEnumerableBreweryyDTOAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    { 
                        new Brewery(){ Name = "Brewery"}
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.IsInstanceOfType(result, typeof(IEnumerable<BreweryDTO>));
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnNullBreweryIfModelHasNoNameAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnNullBreweryIfModelHasNoNameAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfNoBrewryAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfNoBrewryAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfBreweryIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfBreweryIdNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.GetAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfBreweryModelConversionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfBreweryModelConversionFailsAsync");
            using (var context = new BOContext(options))
            {
                var brewery = new Brewery();
                context.Breweries.Add(brewery);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnBreweryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnBreweryDTOAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery",
                            Beers = new List<Beer>()
                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.IsInstanceOfType(result, typeof(BreweryDTO)); ;
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnNullIfBreweryInputNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnNullIfBreweryInputNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.CreateAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldRecordBreweryIfNotOnRecordAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldRecordBreweryIfNotOnRecordAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria"
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                await sut.CreateAsync(breweryDTO);
                var dbresult = await context.Breweries.Include(b => b.Country).FirstOrDefaultAsync(c => c.Name == "Brewery");
                //Assert
                Assert.AreEqual(dbresult.Name, "Brewery");
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteRecordBreweryIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordIfExist");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria"
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
                var brewery = new Brewery()
                {
                    Name = "Brewery",
                    Country = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria"),
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true
                };
                context.Breweries.Add(brewery);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                await sut.CreateAsync(breweryDTO);
                var dbresult = await context.Breweries.FirstOrDefaultAsync(c => c.Name == "Brewery");
                //Assert
                Assert.AreEqual(dbresult.Name, "Brewery");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteRecordBeersIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordBeersIfExist");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true,
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery",
                            DeletedOn = DateTime.UtcNow,
                            IsDeleted = true,

                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
                var beer = new Beer()
                {
                    Name = "Carlsberg",
                    ABV = 5,
                    Country = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria"),
                    Brewery = await context.Breweries.FirstOrDefaultAsync(b => b.Name == "Brewery"),
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true,
                    Style = new BeerStyle()
                    {
                        Name = "Ale",
                        Description = "Some description"
                    },
                    Rating = 5
                };
                context.Beers.Add(beer);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                await sut.CreateAsync(breweryDTO);
                var dbresult = await context.Breweries.FirstOrDefaultAsync(c => c.Name == "Brewery");
                var dbBeerResult = await context.Beers
                    .Include(b => b.Style)
                    .Include(b => b.Reviews)
                    .Include(b => b.Country)
                    .Include(b => b.WishLists)
                    .Include(b => b.DrankLists)
                    .FirstOrDefaultAsync(b => b.Name == "Carlsberg");
                //Assert
                Assert.AreEqual(dbresult.Name, "Brewery");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
                Assert.AreEqual(dbBeerResult.Name, "Carlsberg");
                Assert.AreEqual(dbBeerResult.DeletedOn, null);
                Assert.AreEqual(dbBeerResult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnBreweryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnBreweryDTOAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery"
                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.CreateAsync(breweryDTO);
                //Assert
                Assert.IsInstanceOfType(result, typeof(BreweryDTO));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnModifiedBreweryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnModifiedBreweryDTOAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery"
                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.CreateAsync(breweryDTO);
                var dbresult = await context.Breweries.FirstOrDefaultAsync(c => c.Name == "Brewery");
                //Assert
                Assert.AreEqual(result.ID, dbresult.ID);
                Assert.AreEqual(result.Name, dbresult.Name);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfBreweryIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfBreweryIdNullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.UpdateAsync(null, breweryDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfBreweryDTONullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfBreweryDTONullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.UpdateAsync(1, null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfBreweryNonExistantAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfBreweryNonExistantAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.UpdateAsync(1, breweryDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldChangeNameAndCountryOfBreweryAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldChangeNameOfBreweryAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery"
                        }
                    }
                };
                var country2 = new Country() { Name = "Belgium" };
                context.Countries.Add(country);
                context.Countries.Add(country2);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery2",
                    Country = "Belgium"
                };
                //Act
                var sut = new BreweryServices(context);
                await sut.UpdateAsync(1, breweryDTO);
                var dbresult = await context.Breweries.Include(b => b.Country).FirstOrDefaultAsync(b => b.ID == 1);
                //Assert
                Assert.AreEqual(dbresult.Name, "Brewery2");
                Assert.AreEqual(dbresult.Country.Name, "Belgium");
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnFalseIfBreweryDoesntExistAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnFalseIfBreweryDoesntExistAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, false);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteBreweryIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteBreweryIfExist");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery"
                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Breweries.FirstOrDefaultAsync(c => c.Name == "Brewery");
                //Assert
                Assert.AreEqual(dbresult.Name, "Brewery");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteRecordBeersIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteRecordBeersIfExist");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true,
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery",
                            DeletedOn = DateTime.UtcNow,
                            IsDeleted = true,

                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
                var beer = new Beer()
                {
                    Name = "Carlsberg",
                    ABV = 5,
                    Country = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria"),
                    Brewery = await context.Breweries.FirstOrDefaultAsync(b => b.Name == "Brewery"),
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true,
                    Style = new BeerStyle()
                    {
                        Name = "Ale",
                        Description = "Some description"
                    },
                    Rating = 5
                };
                context.Beers.Add(beer);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var breweryDTO = new BreweryDTO()
                {
                    Name = "Brewery",
                    Country = "Bulgaria"
                };
                //Act
                var sut = new BreweryServices(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Breweries.FirstOrDefaultAsync(c => c.Name == "Brewery");
                var dbBeerResult = await context.Beers
                    .Include(b => b.Style)
                    .Include(b => b.Reviews)
                    .Include(b => b.Country)
                    .Include(b => b.WishLists)
                    .Include(b => b.DrankLists)
                    .FirstOrDefaultAsync(b => b.Name == "Carlsberg");
                //Assert
                Assert.AreEqual(dbresult.Name, "Brewery");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
                Assert.AreEqual(dbBeerResult.Name, "Carlsberg");
                Assert.AreEqual(dbBeerResult.DeletedOn, dbBeerResult.ModifiedOn);
                Assert.AreEqual(dbBeerResult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueIfBrewerySucceded()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnTrueIfBrewerySucceded");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    Breweries = new List<Brewery>()
                    {
                        new Brewery()
                        {
                            Name = "Brewery"
                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BreweryServices(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, true);
            }
        }
    }
}
