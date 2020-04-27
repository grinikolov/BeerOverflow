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
    public class BeerStyleServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_ShouldReturnEmptyIfNoStylesAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnEmptyIfNoStylesAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnIEnumerableBeerStyleDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnIEnumerableBeerStyleDTOAsync");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.IsInstanceOfType(result, typeof(IEnumerable<BeerStyleDTO>));
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnNullIfModelStyleHasNoNameFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnNullIfModelStyleHasNoNameFailsAsync");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle();
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfNoStyleAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfNoStyleAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfStyleIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfStyleIdNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.GetAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfModelStyleConversionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfModelStyleConversionFailsAsync");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle();
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnBeerStyleDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnBeerStyleDTOAsync");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.IsInstanceOfType(result, typeof(BeerStyleDTO)); ;
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnNullIfStyleInputNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnNullIfStyleInputNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.CreateAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldRecordStyleIfNotOnRecordAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldRecordStyleIfNotOnRecordAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                //Act
                var sut = new BeerStylesService(context);
                await sut.CreateAsync(styleDTO);
                var dbresult = await context.BeerStyles.FirstOrDefaultAsync(c => c.Name == "Ale");
                //Assert
                Assert.AreEqual(dbresult.Name, "Ale");
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteStyleRecordIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteStyleRecordIfExist");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle()
                {
                    Name = "Ale",
                    Description = "This description",
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true
                };
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                //Act
                var sut = new BeerStylesService(context);
                await sut.CreateAsync(styleDTO);
                var dbresult = await context.BeerStyles.FirstOrDefaultAsync(c => c.Name == "Ale");
                //Assert
                Assert.AreEqual(dbresult.Name, "Ale");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteBeerRecordIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteBeerRecordIfExist");
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
                        Description = "Some description",
                        DeletedOn = DateTime.UtcNow,
                        IsDeleted = true
                        
                    },
                    Rating = 5
                };
                context.Beers.Add(beer);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                //Act
                var sut = new BeerStylesService(context);
                await sut.CreateAsync(styleDTO);
                var dbresult = await context.BeerStyles.FirstOrDefaultAsync(c => c.Name == "Ale");
                var dbBeerResult = await context.Beers.FirstOrDefaultAsync(b => b.StyleID == dbresult.ID);
                //Assert
                Assert.AreEqual(dbresult.Name, "Ale");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
                Assert.AreEqual(dbBeerResult.Name, "Carlsberg");
                Assert.AreEqual(dbBeerResult.DeletedOn, null);
                Assert.AreEqual(dbBeerResult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnBeerStyleDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnBeerStyleDTOAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.CreateAsync(styleDTO);
                //Assert
                Assert.IsInstanceOfType(result, typeof(BeerStyleDTO));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnModifiedBeerStyleDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnModifiedBeerStyleDTOAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.CreateAsync(styleDTO);
                var dbresult = await context.BeerStyles.FirstOrDefaultAsync(c => c.Name == "Ale");
                //Assert
                Assert.AreEqual(result.ID, dbresult.ID);
                Assert.AreEqual(result.Name, dbresult.Name);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfStyleIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfStyleIdNullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.UpdateAsync(null, styleDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfStyleDTONullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfStyleDTONullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.UpdateAsync(1, null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfStyleNonExistantAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfStyleNonExistantAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.UpdateAsync(1, styleDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldChangeNameOfStyleAndDescriptionAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldChangeNameOfStyleAndDescriptionAsync");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var styleDTO = new BeerStyleDTO()
                {
                    Name = "Ale2",
                    Description = "This description2"
                };
                //Act
                var sut = new BeerStylesService(context);
                await sut.UpdateAsync(1, styleDTO);
                var dbresult = await context.BeerStyles.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Name, "Ale2");
                Assert.AreEqual(dbresult.Description, "This description2");
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnFalseIfStyleDoesntExistAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnFalseIfStyleDoesntExistAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, false);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteStyleIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteStyleIfExist");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.BeerStyles.FirstOrDefaultAsync(c => c.Name == "Ale");
                //Assert
                Assert.AreEqual(dbresult.Name, "Ale");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteRecordBeersOfStyleIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteRecordBeersOfStyleIfExist");
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
                    Style = new BeerStyle()
                    {
                        Name = "Ale",
                        Description = "Some description",
                    },
                    Rating = 5
                };
                context.Beers.Add(beer);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.BeerStyles.FirstOrDefaultAsync(c => c.Name == "Ale");
                var dbBeerResult = await context.Beers.FirstOrDefaultAsync(b => b.StyleID == dbresult.ID);
                //Assert
                Assert.AreEqual(dbresult.Name, "Ale");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
                Assert.AreEqual(dbBeerResult.Name, "Carlsberg");
                Assert.AreEqual(dbBeerResult.DeletedOn, dbBeerResult.ModifiedOn);
                Assert.AreEqual(dbBeerResult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueStyleIfSucceded()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnTrueStyleIfSucceded");
            using (var context = new BOContext(options))
            {
                var style = new BeerStyle()
                {
                    Name = "Ale",
                    Description = "This description"
                };
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerStylesService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, true);
            }
        }
    }
}
