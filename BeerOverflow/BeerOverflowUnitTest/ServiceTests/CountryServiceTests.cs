using BeerOverflow.Models;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowUnitTest
{
    [TestClass]
    public class CountryServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_ShouldReturnEmptyIfNoCountriesAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnEmptyIfNoCountriesAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnIEnumerableCountryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnIEnumerableCountryDTOAsync");
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
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.IsInstanceOfType(result, typeof(IEnumerable<CountryDTO>));
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnNullIfModelConversionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnNullIfModelConversionFailsAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country();
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfNoCountryAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfNoCountryAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfIdNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfModelConversionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfModelConversionFailsAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country();
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnCountryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnIEnumerableCountryDTOAsync");
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
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.IsInstanceOfType(result, typeof(CountryDTO)); ;
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnNullIfInputNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnNullIfInputNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.CreateAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldRecordCountryIfNotOnRecordAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnNullIfInputNullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var countryDTO = new CountryDTO()
                {
                    Name = "Bulgaria"
                };
                //Act
                var sut = new CountriesService(context);
                await sut.CreateAsync(countryDTO);
                var dbresult =await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria");
                //Assert
                Assert.AreEqual(dbresult.Name, "Bulgaria");
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteRecordIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordIfExist");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria",
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var countryDTO = new CountryDTO()
                {
                    Name = "Bulgaria"
                };
                //Act
                var sut = new CountriesService(context);
                await sut.CreateAsync(countryDTO);
                var dbresult = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria");
                //Assert
                Assert.AreEqual(dbresult.Name, "Bulgaria");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteRecordBreweriesIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordBreweriesIfExist");
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
                            Beers = new List<Beer>()
                        }
                    }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var countryDTO = new CountryDTO()
                {
                    Name = "Bulgaria"
                };
                //Act
                var sut = new CountriesService(context);
                await sut.CreateAsync(countryDTO);
                var dbresult = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria");
                var dbBreweryResult = await context.Breweries.Include(b => b.Beers).FirstOrDefaultAsync(b => b.Name == "Brewery");
                //Assert
                Assert.AreEqual(dbresult.Name, "Bulgaria");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
                Assert.AreEqual(dbBreweryResult.Name, "Brewery");
                Assert.AreEqual(dbBreweryResult.DeletedOn, null);
                Assert.AreEqual(dbBreweryResult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnCountryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnCountryDTOAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var countryDTO = new CountryDTO()
                {
                    Name = "Bulgaria"
                };
                //Act
                var sut = new CountriesService(context);
                var result = await sut.CreateAsync(countryDTO);
                //Assert
                Assert.IsInstanceOfType(result, typeof(CountryDTO));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnModifiedCountryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnModifiedCountryDTOAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var countryDTO = new CountryDTO()
                {
                    Name = "Bulgaria"
                };
                //Act
                var sut = new CountriesService(context);
                var result = await sut.CreateAsync(countryDTO);
                var dbresult = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria");
                //Assert
                Assert.AreEqual(result.ID, dbresult.ID);
                Assert.AreEqual(result.Name, dbresult.Name);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfIdNullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var countryDTO = new CountryDTO()
                {
                    Name = "Bulgaria"
                };
                //Act
                var sut = new CountriesService(context);
                var result = await sut.UpdateAsync(null, countryDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfCountryNonExistantAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfCountryNonExistantAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                var countryDTO = new CountryDTO()
                {
                    Name = "Bulgaria"
                };
                //Act
                var sut = new CountriesService(context);
                var result = await sut.UpdateAsync(1, countryDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldChangeNameOfCountryAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldChangeNameOfCountryAsync");
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
                var countryDTO = new CountryDTO()
                {
                    Name = "Belgium"
                };
                //Act
                var sut = new CountriesService(context);
                var result = await sut.UpdateAsync(1, countryDTO);
                var dbresult = await context.Countries.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Name, "Belgium");
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnFalseIfCountryDoesntExistAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnFalseIfCountryDoesntExistAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, false);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteCountryIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteCountryIfExist");
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
                //Act
                var sut = new CountriesService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria");
                //Assert
                Assert.AreEqual(dbresult.Name, "Bulgaria");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteRecordBreweriesIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteRecordBreweriesIfExist");
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
                var sut = new CountriesService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Countries.FirstOrDefaultAsync(c => c.Name == "Bulgaria");
                var dbBreweryResult = await context.Breweries.Include(b => b.Beers).FirstOrDefaultAsync(b => b.Name == "Brewery");
                //Assert
                Assert.AreEqual(dbresult.Name, "Bulgaria");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
                Assert.AreEqual(dbBreweryResult.Name, "Brewery");
                Assert.AreEqual(dbBreweryResult.DeletedOn, dbBreweryResult.ModifiedOn);
                Assert.AreEqual(dbBreweryResult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueIfSucceded()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnTrueIfSucceded");
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
                //Act
                var sut = new CountriesService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, true);
            }
        }
    }
}
