using BeerOverflow.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.DTOs;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeerOverflowUnitTest.Service.MappersTests
{
    [TestClass]
    public class UserMappersTests
    {
        [TestMethod]
        public void MapUserToDTO_ShouldReturnUserDTO()
        {
            //Arrange
            var cut = new User();
            //Act
            var sut = cut.MapUserToDTO();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(UserDTO));
        }

        [TestMethod]
        public void MapUserToDTO_ShouldReturnCorrectNameAndID()
        {
            var cut = new User()
            {
                Id = 1,
                Name = "SuperMan",
                Password = "123qwe"
            };
            //Act
            var sut = cut.MapUserToDTO();
            //Assert
            Assert.AreEqual(1, sut.ID);
            Assert.AreEqual("SuperMan", sut.Name);
        }

        [TestMethod]
        public void MapUserToDTO_ShouldReturnCorrectDrankList()
        {
            var beer = new Beer()
            {
                ID = 1,
                Name = "Carlsberg",
            };
            var cut = new User()
            {
                Id = 1,
                Name = "SuperMan",
                Password = "123qwe",
                DrankLists = new List<DrankList>(),
            };
            cut.DrankLists.Add(new DrankList()
            {
                BeerID = beer.ID,
                Beer = beer,
                UserID = cut.Id,
                User = cut
            });

            //Act
            var sut = cut.MapUserToDTO();
            //Assert
            Assert.AreEqual(sut.DrankLists.ToList().Count, 1);
            Assert.AreEqual(sut.DrankLists.ToList()[0].BeerName, beer.Name);
            Assert.AreEqual(sut.DrankLists.ToList()[0].BeerID, beer.ID);
        }


        [TestMethod]
        public void MapUserToDTO_ShouldReturnCorrectWishList()
        {
            var beer = new Beer()
            {
                ID = 1,
                Name = "Carlsberg",
            };
            var cut = new User()
            {
                Id = 1,
                Name = "SuperMan",
                Password = "123qwe",
                DrankLists = new List<DrankList>(),
                WishLists = new List<WishList>(),

            };
            cut.WishLists.Add(new WishList()
            {
                BeerID = beer.ID,
                Beer = beer,
                UserID = cut.Id,
                User = cut
            });

            //Act
            var sut = cut.MapUserToDTO();
            //Assert
            Assert.AreEqual(sut.WishLists.ToList().Count, 1);
            Assert.AreEqual(sut.WishLists.ToList()[0].BeerName, beer.Name);
            Assert.AreEqual(sut.WishLists.ToList()[0].BeerID, beer.ID);
        }


        [TestMethod]
        public void MapUserToDTO_ShouldReturnCorrectReviewsList()
        {
            var beer = new Beer()
            {
                ID = 1,
                Name = "Carlsberg",
            };
            var cut = new User()
            {
                Id = 1,
                Name = "SuperMan",
                Password = "123qwe",
                ReviewList = new List<Review>(),

            };
            cut.ReviewList.Add(new Review()
            {
                BeerID = beer.ID,
                Beer = beer,
                UserID = cut.Id,
                User = cut,
                Description = "Great beer."
            });

            //Act
            var sut = cut.MapUserToDTO();
            //Assert
            Assert.AreEqual(sut.ReviewsList.ToList().Count, 1);
            Assert.AreEqual(sut.ReviewsList.ToList()[0].Beer.Name, beer.Name);
            Assert.AreEqual(sut.ReviewsList.ToList()[0].Description, "Great beer.");
        }
        [TestMethod]
        public void MapUserToDTO_ShouldReturnCorrectCommentsList()
        {
            var cut = new User()
            {
                Id = 1,
                Name = "SuperMan",
                Password = "123qwe",
                ReviewList = new List<Review>(),
                CommentList = new List<Comment>(),
            };

            var beer = new Beer()
            {
                ID = 1,
                Name = "Carlsberg",
            };

            var review = new Review()
            {
                ID = 1,
                BeerID = beer.ID,
                Beer = beer,
                UserID = cut.Id,
                User = cut,
                Description = "Great beer."
            };

            cut.ReviewList.Add(review);

            cut.CommentList.Add(new Comment()
            {
                ID = 1,
                // BeerID = beer.ID,
                Beer = beer,
                //UserID = cut.Id,
                User = cut,
                ReviewID = 1,
                Description = "Thank you for the review."
            });


            //Act
            var sut = cut.MapUserToDTO();
            //Assert
            Assert.AreEqual(sut.CommentsList.ToList().Count, 1);
            Assert.AreEqual(sut.CommentsList.ToList()[0].ReviewID, review.ID);
            Assert.AreEqual(sut.CommentsList.ToList()[0].Description, "Thank you for the review.");
        }


        [TestMethod]
        public void MapDTOToUser_ShouldReturnCorrectNamePassword()
        {
            var cut = new UserDTO()
            {
                Name = "SuperMan",
                Password = "123qwe"
            };
            //Act
            var sut = cut.MapToUser();
            //Assert
            Assert.AreEqual("SuperMan", sut.Name);
            Assert.AreEqual(cut.Password, sut.Password);
        }
    }
}
