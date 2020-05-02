using BeerOverflow.Models;
using Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Services.DTOs;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflowUnitTest.ServiceTests
{
    [TestClass]
    public class UsersServiceTests
    {
        [TestMethod]
        public void GetUserShould_ReturnNull_whenNoSuchUser()
        {
            //Arrange
            var options = InMemory.GetOptions("GetUserShould_ReturnNull_whenNoSuchUser");
            using (var context = new BOContext(options))
            {
            }
            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.GetUser(55).Result;
                //Assert
                Assert.IsNull(result);
            }
        }
        [TestMethod]
        public void GetUserShould_ReturnCorrectUser()
        {
            //Arrange
            var options = InMemory.GetOptions("GetUserShould_ReturnCorrectUser");
            using (var context = new BOContext(options))
            {
                var cut = new User()
                {
                    Id = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                    ReviewList = new List<Review>(),
                    CommentList = new List<Comment>(),
                    DrankLists = new List<DrankList>(),
                    WishLists = new List<WishList>(),
                };
                context.Users.Add(cut);
                context.SaveChanges();
            }
            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.GetUser(1).Result;
                //Assert
                Assert.AreEqual(result.Name, "SuperMan");
            }
        }

        [TestMethod]
        public void GetAllUsersShould_ReturnEmpty_whenNoSuchUser()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllUsersShould_ReturnEmpty_whenNoSuchUser");
            using (var context = new BOContext(options))
            {
            }
            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.GetAllUsers().Result;
                //Assert
                Assert.AreEqual(0, result.ToList().Count);
            }
        }
        [TestMethod]
        public void GetAllUsersShould_ReturnCorrectUser()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAllUsersShould_ReturnCorrectUser");
            using (var context = new BOContext(options))
            {
                var cut = new User()
                {
                    Id = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                    ReviewList = new List<Review>(),
                    CommentList = new List<Comment>(),
                    DrankLists = new List<DrankList>(),
                    WishLists = new List<WishList>(),
                };
                context.Users.Add(cut);
                context.SaveChanges();
            }
            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.GetAllUsers().Result.ToList();
                //Assert
                Assert.AreEqual(result.Count, 1);
                Assert.AreEqual(result[0].Name, "SuperMan");
            }
        }

        [TestMethod]
        public void UpdateUserShould_ReturnCorrectUser()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateUserShould_ReturnCorrectUser");
            using (var context = new BOContext(options))
            {
                var cut = new User()
                {
                    Id = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                    ReviewList = new List<Review>(),
                    CommentList = new List<Comment>(),
                    DrankLists = new List<DrankList>(),
                    WishLists = new List<WishList>(),
                };
                context.Users.Add(cut);
                context.SaveChanges();
            }
            var updatedUser = new UserDTO()
            {
                Name = "Batman",
                Password = "qwe123",
            };
            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.UpdateUser(1, updatedUser);
                //Assert
                Assert.AreEqual(updatedUser.Name, result.Name);
                Assert.AreEqual(updatedUser.Password, result.Password);
                //Assert.IsNotNull(result.ModifiedOn);
            }
        }

        [TestMethod]
        public void UpdateUserAsyncShould_ReturnCorrectUser()
        {
            //Arrange
            var options = InMemory.GetOptions("UpdateUserAsyncShould_ReturnCorrectUser");
            using (var context = new BOContext(options))
            {
                var cut = new User()
                {
                    Id = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                    ReviewList = new List<Review>(),
                    CommentList = new List<Comment>(),
                    DrankLists = new List<DrankList>(),
                    WishLists = new List<WishList>(),
                };
                context.Users.Add(cut);
                context.SaveChanges();
            }
            var updatedUser = new UserDTO()
            {
                Name = "Batman",
                Password = "qwe123",
            };
            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.UpdateUserAsync(1, updatedUser).Result;
                //Assert
                Assert.AreEqual(updatedUser.Name, result.Name);
                Assert.AreEqual(updatedUser.Password, result.Password);
                //Assert.IsNotNull(result.ModifiedOn);
            }
        }

        //[TestMethod]
        //public void CreateUserShould_ReturnNull_whenNotValidInput()
        //{
        //    //Arrange
        //    var options = InMemory.GetOptions("CreateUserShould_ReturnNull_whenNotValidInput");
        //    using (var context = new BOContext(options))
        //    {
        //    }
        //    UserDTO model= new UserDTO() { Password = "123qwe" };

        //    using (var context = new BOContext(options))
        //    {
        //        //Act
        //        var sut = new UsersService(context);
        //        var result = sut.CreateUser(model).Result;
        //        //Assert
        //       Assert.IsNull(result);
        //    }
        //}
        [TestMethod]
        public void CreateUserShould_ReturnCorrectUser()
        {
            //Arrange
            var options = InMemory.GetOptions("CreateUserShould_ReturnCorrectUser");
            using (var context = new BOContext(options))
            {
            }
            var model = new UserDTO()
            {
                Name = "Batman",
                Password = "qwe123",
            };

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.CreateUser(model).Result;
                //Assert
                Assert.AreEqual(model.Name, result.Name);
                Assert.AreEqual(model.Password, result.Password);
            }
        }



        [TestMethod]
        public void DeleteUserShould_ReturnTrue_whenDeletedUser()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteUserShould_ReturnTrue_whenDeletedUser");
            using (var context = new BOContext(options))
            {
                var cut = new User()
                {
                    Id = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                };

                var user = new User()
                {
                    Id = 2,
                    Name = "Batman",
                    Password = "123qwe",
                };
                context.Users.Add(cut);
                context.Users.Add(user);
                context.SaveChanges();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.DeleteUser(1).Result;
                //Assert
                var deletedUsers = context.Users.Where(u => u.IsDeleted == true).Count();
                var activeUsers = context.Users.Where(u => u.IsDeleted == false).Count();
                Assert.IsTrue(result);
                Assert.AreEqual(1, deletedUsers);
                Assert.AreEqual(1, activeUsers);
            }
        }

        [TestMethod]
        public void DeleteUserShould_ReturnFalse_whenNotDeletedUser()
        {
            //Arrange
            var options = InMemory.GetOptions("DeleteUserShould_ReturnFalse_whenNotDeletedUser");
            using (var context = new BOContext(options))
            {
                var cut = new User()
                {
                    Id = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                };

                context.Users.Add(cut);
                context.SaveChanges();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.DeleteUser(55).Result;
                //Assert
                var deletedUsers = context.Users.Where(u => u.IsDeleted == true).Count();
                var activeUsers = context.Users.Where(u => u.IsDeleted == false).Count();
                Assert.IsFalse(result);
                Assert.AreEqual(0, deletedUsers);
                Assert.AreEqual(1, activeUsers);
            }
        }


        [TestMethod]
        public void RateShould_WriteRating_WhenValid()
        {
            //Arrange
            var options = InMemory.GetOptions("RateShould_WriteRating_WhenValid");
            using (var context = new BOContext(options))
            {
                var user = new User()
                {
                    Id = 1,
                    IDOld = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                };

                var beer = new Beer()
                {
                    ID = 1,
                    ABV = 4.5f,
                    Name = "Carlsberg",
                    Country = new Country() { Name = "Germany" },
                    Style = new BeerStyle() { Name = "Ale" }
                };

                context.Users.Add(user);
                context.Beers.Add(beer);
                context.SaveChanges();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.Rate(1, 1, 5).Result;
                var beerWithNewRating = context.Beers.Find(1);
                //Assert
                Assert.AreEqual(5, beerWithNewRating.Rating);
            }
        }


        [TestMethod]
        public void RateShould_RecalculateRating_WhenValid()
        {
            //Arrange
            var options = InMemory.GetOptions("RateShould_RecalculateRating_WhenValid");
            using (var context = new BOContext(options))
            {
                var user = new User()
                {
                    Id = 1,
                    IDOld = 1,
                    Name = "SuperMan",
                    Password = "123qwe",
                };

                var user2 = new User()
                {
                    Id = 2,
                    IDOld = 2,
                    Name = "Batman",
                    Password = "123qwe",
                };


                var beer = new Beer()
                {
                    ID = 1,
                    ABV = 4.5f,
                    Name = "Carlsberg",
                    Country = new Country() { Name = "Germany" },
                    Style = new BeerStyle() { Name = "Ale" }
                };
                var firstNewRating = new BeerUserRating()
                {
                    BeerID = beer.ID,
                    Beer = beer,
                    UserID = user.IDOld,
                    User = user,
                    Rating = 2,
                }; 
                context.Users.Add(user);
                context.Users.Add(user2);
                context.Beers.Add(beer);
                context.BeerUserRatings.Add(firstNewRating);
                context.SaveChanges();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new UsersService(context);
                var result = sut.Rate(2, 1, 5).Result;
                var beerWithNewRating = context.Beers.Find(1);
                //Assert
                Assert.AreEqual(3.5, beerWithNewRating.Rating);
            }
        }
    }
}
