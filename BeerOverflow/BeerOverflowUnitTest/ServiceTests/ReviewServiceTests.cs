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
    public class ReviewServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_ShouldReturnEmptyIfNoReviewsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnEmptyIfNoReviewsAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnIEnumerableReviewDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnIEnumerableReviewDTOAsync");
            using (var context = new BOContext(options))
            {
                var review = new Review
                {
                    Description = "Great",
                    Beer = new Beer() { Name = "Carlsberg" },
                    User = new User() { Name = "SuperMan" }
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.IsInstanceOfType(result, typeof(IEnumerable<ReviewDTO>));
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnNullIfModelReviewHasNoBeerOrUserFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnNullIfModelReviewHasNoBeerOrUserFailsAsync");
            using (var context = new BOContext(options))
            {
                var review = new Review();
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfNoReviewAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfNoReviewAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfReviewIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfReviewIdNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.GetAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfReviewModelConversionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfReviewModelConversionFailsAsync");
            using (var context = new BOContext(options))
            {
                var review = new Review();
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnReviewDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnReviewDTOAsync");
            using (var context = new BOContext(options))
            {
                var review = new Review
                {
                    Description = "Great",
                    Beer = new Beer() { Name = "Carlsberg" },
                    User = new User() { Name = "SuperMan" }
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.IsInstanceOfType(result, typeof(ReviewDTO)); ;
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnNullIfReviewInputNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnNullIfReviewInputNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.CreateAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldRecordReviewIfNotOnRecordAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldRecordReviewIfNotOnRecordAsync");
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
                var user = new User() { IDOld = 1, Name = "SuperMan" };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                await sut.CreateAsync(reviewDTO);
                var dbresult = await context.Reviews.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Great");
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteReviewRecordIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteReviewRecordIfExist");
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
                var user = new User() { IDOld = 1, Name = "SuperMan" };
                context.Users.Add(user);
                await context.SaveChangesAsync();
                var review = new Review
                {
                    Description = "Great",
                    Beer = await context.Beers.FirstOrDefaultAsync(b => b.Name == "Carlsberg"),
                    User = await context.Users.FirstOrDefaultAsync(b => b.Name == "SuperMan"),
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                await sut.CreateAsync(reviewDTO);
                var dbresult = await context.Reviews.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Great");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteRecordReviewCommentsIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordReviewCommentsIfExist");
            using (var context = new BOContext(options))
            {
                var review = new Review
                {
                    Description = "Great",
                    Beer = new Beer() { Name = "Carlsberg" },
                    User = new User() { IDOld = 1, Name = "SuperMan" },
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow,
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Description = "Some description",
                            User = new User(){ IDOld = 2, Name = "Batman"},
                            IsDeleted = true,
                            DeletedOn = DateTime.UtcNow

                        }
                    }
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                await sut.CreateAsync(reviewDTO);
                var dbresult = await context.Reviews.FindAsync(1);
                var dbCommentResult = await context.Comments.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Great");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
                Assert.AreEqual(dbCommentResult.Description, "Some description");
                Assert.AreEqual(dbCommentResult.DeletedOn, null);
                Assert.AreEqual(dbCommentResult.IsDeleted, false);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnReviewDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnReviewDTOAsync");
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
                var user = new User() { IDOld = 1, Name = "SuperMan" };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.CreateAsync(reviewDTO);
                //Assert
                Assert.IsInstanceOfType(result, typeof(ReviewDTO));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnModifiedReviewDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnModifiedReviewDTOAsync");
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
                var user = new User() { IDOld = 1, Name = "SuperMan" };
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.CreateAsync(reviewDTO);
                var dbresult = await context.Reviews.FindAsync(1);
                //Assert
                Assert.AreEqual(result.ID, dbresult.ID);
                Assert.AreEqual(result.Description, dbresult.Description);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfReviewIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfReviewIdNullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.UpdateAsync(null, reviewDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfReviewDTONullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfReviewDTONullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.UpdateAsync(1, null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfReviewNonExistantAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfReviewNonExistantAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.UpdateAsync(1, reviewDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldChangeDescriptionOfReviewAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldChangeDescriptionOfReviewAsync");
            using (var context = new BOContext(options))
            {
                var review = new Review
                {
                    Description = "Great",
                    Beer = new Beer() { Name = "Carlsberg" },
                    User = new User() { IDOld = 1, Name = "SuperMan" },
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var reviewDTO = new ReviewDTO
                {
                    Description = "Great2",
                    Beer = new BeerDTO() { Name = "Carlsberg" },
                    User = new UserDTO() { Name = "SuperMan" }
                };
                //Act
                var sut = new ReviewsService(context);
                await sut.UpdateAsync(1, reviewDTO);
                var dbresult = await context.Reviews.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Great2");
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnFalseIfReviewDoesntExistAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnFalseIfReviewDoesntExistAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, false);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteReviewIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteReviewIfExist");
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
                var user = new User() { Name = "SuperMan" };
                context.Users.Add(user);
                await context.SaveChangesAsync();
                var review = new Review
                {
                    Description = "Great",
                    Beer = await context.Beers.FirstOrDefaultAsync(b => b.Name == "Carlsberg"),
                    User = await context.Users.FirstOrDefaultAsync(b => b.Name == "SuperMan"),
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Reviews.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Great");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteRecordCommentsIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteRecordCommentsIfExist");
            using (var context = new BOContext(options))
            {
                var review = new Review
                {
                    Description = "Great",
                    Beer = new Beer() { Name = "Carlsberg" },
                    User = new User() { Name = "SuperMan" },
                    Comments = new List<Comment>()
                    {
                        new Comment()
                        {
                            Description = "Some description",
                            User = new User(){ Name = "Batman"},
                        }
                    }
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Reviews.FindAsync(1);
                var dbCommentResult = await context.Comments.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Great");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
                Assert.AreEqual(dbCommentResult.Description, "Some description");
                Assert.AreEqual(dbCommentResult.DeletedOn, dbCommentResult.ModifiedOn);
                Assert.AreEqual(dbCommentResult.IsDeleted, true);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueIfReviewSucceded()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnTrueIfSucceded");
            using (var context = new BOContext(options))
            {
                var review = new Review
                {
                    Description = "Great",
                    Beer = new Beer() { Name = "Carlsberg" },
                    User = new User() { Name = "SuperMan" },
                };
                context.Reviews.Add(review);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new ReviewsService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, true);
            }
        }
    }
}
