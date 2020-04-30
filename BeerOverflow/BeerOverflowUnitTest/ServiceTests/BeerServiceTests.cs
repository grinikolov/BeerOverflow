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
    public class BeerServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_ShouldReturnEmptyIfNoBeersAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnEmptyIfNoBeersAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnIEnumerableBeerDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnIEnumerableBeerDTOAsync");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.IsInstanceOfType(result, typeof(IEnumerable<BeerDTO>));
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnNullBeerIfModelHasNoNameAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnNullBeerIfModelHasNoNameAsync");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                }
                            }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfNoBeerAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfNoBeerAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfBeerIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfBeerIdNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.GetAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfBeerModelConversionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfBeerModelConversionFailsAsync");
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
                                    {
                                        new Beer()
                                    }
                                }
                            }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnBeerDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnBeerDTOAsync");
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
                                    {
                                        new Beer()
                                        {
                                            Name = "Carlsberg",
                                            ABV = 4.5f,
                                            Rating = 2,
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                }
                            }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.IsInstanceOfType(result, typeof(BeerDTO)); ;
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnNullIfBeerInputNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnNullIfBeerInputNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.CreateAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldRecordBeerIfNotOnRecordAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldRecordBeerIfNotOnRecordAsync");
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
                                    //Beers = new List<Beer>()
                                    //{
                                    //    new Beer()
                                    //    {
                                    //        ABV = 4.5f,
                                    //        Rating = 2,
                                    //        Country = new Country() { Name = "Germany" },
                                    //        Style = new BeerStyle() { Name = "Ale" }
                                    //    }
                                    //}
                                }
                            }
                };
                context.Countries.Add(country);
                context.Countries.Add(new Country() { Name = "Germany" });
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 4.5f,
                    Rating = 2,
                    Name = "Carlsberg",
                    Country = new CountryDTO() { Name = "Germany" },
                    Style = new BeerStyleDTO() { Name = "Ale" },
                    Brewery = new BreweryDTO() { Name = "Brewery" }
                };
                //Act
                var sut = new BeerService(context);
                await sut.CreateAsync(beerDTO);
                var dbresult = await context.Beers.FirstOrDefaultAsync(c => c.Name == "Carlsberg");
                //Assert
                Assert.AreEqual(dbresult.Name, "Carlsberg");
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteRecordBeerIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordBeerIfExist");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            DeletedOn = DateTime.UtcNow,
                                            IsDeleted = true,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 4.5f,
                    Rating = 2,
                    Name = "Carlsberg",
                    Country = new CountryDTO() { Name = "Germany" },
                    Style = new BeerStyleDTO() { Name = "Ale" },
                    Brewery = new BreweryDTO() { Name = "Brewery" }
                };
                //Act
                var sut = new BeerService(context);
                await sut.CreateAsync(beerDTO);
                var dbresult = await context.Beers.FirstOrDefaultAsync(c => c.Name == "Carlsberg");
                //Assert
                Assert.AreEqual(dbresult.Name, "Carlsberg");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteRecordReviewIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordReviewIfExist");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            DeletedOn = DateTime.UtcNow,
                                            IsDeleted = true,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
                var review = new Review()
                {
                    DeletedOn = DateTime.UtcNow,
                    IsDeleted = true,
                    Beer = await context.Beers.FirstOrDefaultAsync(b => b.Name == "Carlsberg"),
                    User = new User()
                    {
                        Name = "User",
                        Password = "Password"
                    }
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 4.5f,
                    Rating = 2,
                    Name = "Carlsberg",
                    Country = new CountryDTO() { Name = "Germany" },
                    Style = new BeerStyleDTO() { Name = "Ale" },
                    Brewery = new BreweryDTO() { Name = "Brewery" }
                };
                //Act
                var sut = new BeerService(context);
                await sut.CreateAsync(beerDTO);
                var dbresult = await context.Beers.FirstOrDefaultAsync(b => b.Name == "Carlsberg");
                var dbBeerResult = await context.Reviews
                    .Include(r => r.Beer)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.BeerID == 1);
                //Assert
                Assert.AreEqual(dbresult.Name, "Carlsberg");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
                Assert.AreEqual(dbBeerResult.BeerID, 1);
                Assert.AreEqual(dbBeerResult.DeletedOn, null);
                Assert.AreEqual(dbBeerResult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnBeerDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnBeerDTOAsync");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            DeletedOn = DateTime.UtcNow,
                                            IsDeleted = true,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 4.5f,
                    Rating = 2,
                    Name = "Carlsberg",
                    Country = new CountryDTO() { Name = "Germany" },
                    Style = new BeerStyleDTO() { Name = "Ale" },
                    Brewery = new BreweryDTO() { Name = "Brewery" }
                };
                //Act
                var sut = new BeerService(context);
                var result = await sut.CreateAsync(beerDTO);
                //Assert
                Assert.IsInstanceOfType(result, typeof(BeerDTO));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnModifiedBeerDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnModifiedBeerDTOAsync");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            DeletedOn = DateTime.UtcNow,
                                            IsDeleted = true,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 4.5f,
                    Rating = 2,
                    Name = "Carlsberg",
                    Country = new CountryDTO() { Name = "Germany" },
                    Style = new BeerStyleDTO() { Name = "Ale" },
                    Brewery = new BreweryDTO() { Name = "Brewery" }
                };
                //Act
                var sut = new BeerService(context);
                var result = await sut.CreateAsync(beerDTO);
                var dbresult = await context.Beers.FirstOrDefaultAsync(c => c.Name == "Carlsberg");
                //Assert
                Assert.AreEqual(result.ID, dbresult.ID);
                Assert.AreEqual(result.Name, dbresult.Name);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfBeerIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfBreweryIdNullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 4.5f,
                    Rating = 2,
                    Name = "Carlsberg",
                    Country = new CountryDTO() { Name = "Germany" },
                    Style = new BeerStyleDTO() { Name = "Ale" },
                    Brewery = new BreweryDTO() { Name = "Brewery" }
                };
                //Act
                var sut = new BeerService(context);
                var result = await sut.UpdateAsync(null, beerDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfBeerDTONullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfBeerDTONullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.UpdateAsync(1, null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfBeerNonExistantAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfBeerNonExistantAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 4.5f,
                    Rating = 2,
                    Name = "Carlsberg",
                    Country = new CountryDTO() { Name = "Germany" },
                    Style = new BeerStyleDTO() { Name = "Ale" },
                    Brewery = new BreweryDTO() { Name = "Brewery" }
                };
                //Act
                var sut = new BeerService(context);
                var result = await sut.UpdateAsync(1, beerDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldChangeNameAndCountryAndBreweryAndStyleAndABVOfBeerAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldChangeNameAndCountryAndBreweryAndStyleAndABVOfBeerAsync");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                var brewery = new Brewery() { Name = "Brewery2", Country = new Country() { Name = "Germany" } };
                var style = new BeerStyle() { Name = "Pilsner" };
                context.Countries.Add(country);
                context.Breweries.Add(brewery);
                context.BeerStyles.Add(style);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var beerDTO = new BeerDTO()
                {
                    ABV = 2.5f,
                    Rating = 2,
                    Name = "Ariana",
                    Country = new CountryDTO() { Name = "Bulgaria" },
                    Style = new BeerStyleDTO() { Name = "Pilsner" },
                    Brewery = new BreweryDTO() { Name = "Brewery2" }
                };
                //Act
                var sut = new BeerService(context);
                await sut.UpdateAsync(1, beerDTO);
                var dbresult = await context.Beers
                    .Include(b => b.Country)
                    .Include(b => b.Brewery)
                    .Include(b => b.Style)
                    .FirstOrDefaultAsync(b => b.ID == 1);
                //Assert
                Assert.AreEqual(dbresult.Name, "Ariana");
                Assert.AreEqual(dbresult.Country.Name, "Bulgaria");
                Assert.AreEqual(dbresult.Brewery.Name, "Brewery2");
                Assert.AreEqual(dbresult.Style.Name, "Pilsner");
                Assert.AreEqual(dbresult.ABV, 2.5f);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnFalseIfBeerDoesntExistAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnFalseIfBeerDoesntExistAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, false);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteBeerIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteBeerIfExist");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                }; ;
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Beers.FirstOrDefaultAsync(c => c.Name == "Carlsberg");
                //Assert
                Assert.AreEqual(dbresult.Name, "Carlsberg");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteRecordBeerReviewIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteRecordBeerReviewIfExist");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
                var review = new Review()
                {
                    Beer = await context.Beers.FirstOrDefaultAsync(b => b.Name == "Carlsberg"),
                    User = new User()
                    {
                        Name = "User",
                        Password = "Password"
                    }
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Beers.FirstOrDefaultAsync(c => c.Name == "Carlsberg");
                var dbBeerResult = await context.Reviews
                    .Include(r => r.Beer)
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.BeerID == 1);
                //Assert
                Assert.AreEqual(dbresult.Name, "Carlsberg");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
                Assert.AreEqual(dbBeerResult.BeerID, 1);
                Assert.AreEqual(dbBeerResult.DeletedOn, dbBeerResult.ModifiedOn);
                Assert.AreEqual(dbBeerResult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueIfBeerSucceded()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnTrueIfBeerSucceded");
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
                                    {
                                        new Beer()
                                        {
                                            ABV = 4.5f,
                                            Rating = 2,
                                            Name = "Carlsberg",
                                            Country = new Country() { Name = "Germany" },
                                            Style = new BeerStyle() { Name = "Ale" }
                                        }
                                    }
                                 }
                             }
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new BeerService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, true);
            }
        }
    }
}
