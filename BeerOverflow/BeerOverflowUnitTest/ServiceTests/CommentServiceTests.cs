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
    public class CommentServiceTests
    {
        [TestMethod]
        public async Task GetAllAsync_ShouldReturnEmptyIfNoCommentsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnEmptyIfNoCommentsAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnIEnumerableCommentDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnIEnumerableCommentDTOAsync");
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
                var comment = new Comment()
                {
                    Description = "Gotham",
                    User = await context.Users.FirstOrDefaultAsync(b => b.Name == "SuperMan"),
                    Review = await context.Reviews.FindAsync(1),
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow

                };
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.IsInstanceOfType(result, typeof(IEnumerable<CommentDTO>));
            }
        }

        [TestMethod]
        public async Task GetAllAsync_ShouldReturnNullIfModelCommentHasNoDescriptionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllAsync_ShouldReturnNullIfModelCommentHasNoDescriptionFailsAsync");
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
                var comment = new Comment()
                {
                    User = await context.Users.FirstOrDefaultAsync(b => b.Name == "SuperMan"),
                    Review = await context.Reviews.FindAsync(1),
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow

                };
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.GetAllAsync();
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfNoCommentAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfNoCommentAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfCommentIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfCommentIdNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.GetAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnNullIfCommentModelConversionFailsAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnNullIfCommentModelConversionFailsAsync");
            using (var context = new BOContext(options))
            {
                var comment = new Comment();
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task GetAsync_ShouldReturnCommentDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAsync_ShouldReturnCommentDTOAsync");
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
                var comment = new Comment()
                {
                    Description = "Gotham",
                    User = await context.Users.FirstOrDefaultAsync(b => b.Name == "SuperMan"),
                    Review = await context.Reviews.FindAsync(1),
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow

                };
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.GetAsync(1);
                //Assert
                Assert.IsInstanceOfType(result, typeof(CommentDTO)); ;
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnNullIfCommentInputNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnNullIfCommentInputNullAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.CreateAsync(null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldRecordCommentIfNotOnRecordAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldRecordCommentIfNotOnRecordAsync");
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
                var commentDTO = new CommentDTO
                {
                    Description = "Gotham",
                    User = new UserDTO() { Name = "SuperMan" },
                    Review = new ReviewDTO() { Description = "Description" }
                };
                //Act
                var sut = new CommentService(context);
                await sut.CreateAsync(commentDTO);
                var dbresult = await context.Comments.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Gotham");
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldUndeleteCommentRecordIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteCommentRecordIfExist");
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
                var comment = new Comment()
                {
                    Description = "Gotham",
                    User = await context.Users.FirstOrDefaultAsync(b => b.Name == "SuperMan"),
                    Review = await context.Reviews.FindAsync(1),
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow
                    
                };
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var commentDTO = new CommentDTO
                {
                    Description = "Gotham",
                    UserID = 1,
                    ReviewID = 1
                };
                //Act
                var sut = new CommentService(context);
                await sut.CreateAsync(commentDTO);
                var dbresult = await context.Comments.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Gotham");
                Assert.AreEqual(dbresult.DeletedOn, null);
                Assert.AreEqual(dbresult.IsDeleted, false);
            }
        }

        //[TestMethod]
        //public async Task CreateAsync_ShouldUndeleteRecordCommentsIfExist()
        //{
        //    //Arrange
        //    var options = InMemory.GetOptions("CreateAsync_ShouldUndeleteRecordCommentsIfExist");
        //    using (var context = new BOContext(options))
        //    {
        //        var review = new Review
        //        {
        //            Description = "Great",
        //            Beer = new Beer() { Name = "Carlsberg" },
        //            User = new User() { Name = "SuperMan" },
        //            IsDeleted = true,
        //            DeletedOn = DateTime.UtcNow,
        //            Comments = new List<Comment>()
        //            {
        //                new Comment()
        //                {
        //                    Description = "Some description",
        //                    User = new User(){ Name = "Batman"},
        //                    IsDeleted = true,
        //                    DeletedOn = DateTime.UtcNow

        //                }
        //            }
        //        };
        //        context.Reviews.Add(review);
        //        await context.SaveChangesAsync();
        //    }

        //    using (var context = new BOContext(options))
        //    {
        //        var reviewDTO = new ReviewDTO
        //        {
        //            Description = "Great",
        //            Beer = new BeerDTO() { Name = "Carlsberg" },
        //            User = new UserDTO() { Name = "SuperMan" }
        //        };
        //        //Act
        //        var sut = new ReviewsService(context);
        //        await sut.CreateAsync(reviewDTO);
        //        var dbresult = await context.Reviews.FirstOrDefaultAsync(c => c.Beer.Name == "Carlsberg" && c.User.Name == "SuperMan");
        //        var dbCommentResult = await context.Comments.Include(b => b.User).FirstOrDefaultAsync(b => b.ReviewID == dbresult.ID);
        //        //Assert
        //        Assert.AreEqual(dbresult.Description, "Great");
        //        Assert.AreEqual(dbresult.DeletedOn, null);
        //        Assert.AreEqual(dbresult.IsDeleted, false);
        //        Assert.AreEqual(dbCommentResult.Description, "Some description");
        //        Assert.AreEqual(dbCommentResult.DeletedOn, null);
        //        Assert.AreEqual(dbCommentResult.IsDeleted, false);
        //    }
        //}

        [TestMethod]
        public async Task CreateAsync_ShouldReturnCommentDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnCommentDTOAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var commentDTO = new CommentDTO
                {
                    Description = "Gotham",
                    User = new UserDTO() { Name = "Batman" },
                    Review = new ReviewDTO() { Description = "Description" }
                };
                //Act
                var sut = new CommentService(context);
                var result = await sut.CreateAsync(commentDTO);
                //Assert
                Assert.IsInstanceOfType(result, typeof(CommentDTO));
            }
        }

        [TestMethod]
        public async Task CreateAsync_ShouldReturnModifiedCommentDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateAsync_ShouldReturnModifiedCommentDTOAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var commentDTO = new CommentDTO
                {
                    Description = "Gotham",
                    User = new UserDTO() { Name = "Batman" },
                    Review = new ReviewDTO() { Description = "Description" }
                };
                //Act
                var sut = new CommentService(context);
                var result = await sut.CreateAsync(commentDTO);
                var dbresult = await context.Comments.FindAsync(1);
                //Assert
                Assert.AreEqual(result.ID, dbresult.ID);
                Assert.AreEqual(result.Description, dbresult.Description);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfCommentIdNullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfCommentIdNullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                var commentDTO = new CommentDTO
                {
                    Description = "Gotham",
                    User = new UserDTO() { Name = "Batman" },
                    Review = new ReviewDTO() { Description = "Description" }
                };
                //Act
                var sut = new CommentService(context);
                var result = await sut.UpdateAsync(null, commentDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfCommentDTONullAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfCommentDTONullAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.UpdateAsync(1, null);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldReturnNullIfCommentNonExistantAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldReturnNullIfCommentNonExistantAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                var commentDTO = new CommentDTO
                {
                    Description = "Gotham",
                    User = new UserDTO() { Name = "Batman" },
                    Review = new ReviewDTO() { Description = "Description" }
                };
                //Act
                var sut = new CommentService(context);
                var result = await sut.UpdateAsync(1, commentDTO);
                //Assert
                Assert.AreEqual(result, null);
            }
        }

        [TestMethod]
        public async Task UpdateAsync_ShouldChangeDescriptionOfCommentAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateAsync_ShouldChangeDescriptionOfCommentAsync");
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
                var comment = new Comment()
                {
                    Description = "Gotham",
                    User = await context.Users.FirstOrDefaultAsync(b => b.Name == "SuperMan"),
                    Review = await context.Reviews.FindAsync(1),
                    IsDeleted = true,
                    DeletedOn = DateTime.UtcNow

                };
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                var commentDTO = new CommentDTO
                {
                    Description = "Gotham2",
                    User = new UserDTO() { Name = "SuperMan" },
                    Review = new ReviewDTO() { Description = "Description" }
                };
                //Act
                var sut = new CommentService(context);
                await sut.UpdateAsync(1, commentDTO);
                var dbresult = await context.Comments.FindAsync(1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Gotham2");
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnFalseIfCommentDoesntExistAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnFalseIfCommentDoesntExistAsync");
            using (var context = new BOContext(options))
            {
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, false);
            }
        }

        [TestMethod]
        public async Task DeleteAsync_ShouldDeleteCommentIfExist()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteCommentIfExist");
            using (var context = new BOContext(options))
            {
                var comment = new Comment
                {
                    Description = "Gotham",
                    User = new User() { Name = "Batman" },
                    Review = new Review() { Description = "Description" }
                };
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                await sut.DeleteAsync(1);
                var dbresult = await context.Comments.FirstOrDefaultAsync(c => c.ReviewID == 1 && c.UserID == 1);
                //Assert
                Assert.AreEqual(dbresult.Description, "Gotham");
                Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
                Assert.AreEqual(dbresult.IsDeleted, true);
            }
        }

        //[TestMethod]
        //public async Task DeleteAsync_ShouldDeleteRecordCommentsIfExist()
        //{
        //    //Arrange
        //    var options = InMemory.GetOptions("DeleteAsync_ShouldDeleteRecordCommentsIfExist");
        //    using (var context = new BOContext(options))
        //    {
        //        var review = new Review
        //        {
        //            Description = "Great",
        //            Beer = new Beer() { Name = "Carlsberg" },
        //            User = new User() { Name = "SuperMan" },
        //            Comments = new List<Comment>()
        //            {
        //                new Comment()
        //                {
        //                    Description = "Some description",
        //                    User = new User(){ Name = "Batman"},
        //                }
        //            }
        //        };
        //        context.Reviews.Add(review);
        //        await context.SaveChangesAsync();
        //    }

        //    using (var context = new BOContext(options))
        //    {
        //        //Act
        //        var sut = new ReviewsService(context);
        //        await sut.DeleteAsync(1);
        //        var dbresult = await context.Reviews.FirstOrDefaultAsync(c => c.Beer.Name == "Carlsberg" && c.User.Name == "SuperMan");
        //        var dbCommentResult = await context.Comments.Include(b => b.User).FirstOrDefaultAsync(b => b.ReviewID == dbresult.ID);
        //        //Assert
        //        Assert.AreEqual(dbresult.Description, "Great");
        //        Assert.AreEqual(dbresult.DeletedOn, dbresult.ModifiedOn);
        //        Assert.AreEqual(dbresult.IsDeleted, true);
        //        Assert.AreEqual(dbCommentResult.Description, "Some description");
        //        Assert.AreEqual(dbCommentResult.DeletedOn, dbCommentResult.ModifiedOn);
        //        Assert.AreEqual(dbCommentResult.IsDeleted, true);
        //    }
        //}

        [TestMethod]
        public async Task DeleteAsync_ShouldReturnTrueIfCommentSucceded()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteAsync_ShouldReturnTrueIfCommentSucceded");
            using (var context = new BOContext(options))
            {
                var comment = new Comment
                {
                    Description = "Gotham",
                    User = new User() { Name = "Batman" },
                    Review = new Review() { Description = "Description" }
                };
                context.Comments.Add(comment);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CommentService(context);
                var result = await sut.DeleteAsync(1);
                //Assert
                Assert.AreEqual(result, true);
            }
        }
    }
}
