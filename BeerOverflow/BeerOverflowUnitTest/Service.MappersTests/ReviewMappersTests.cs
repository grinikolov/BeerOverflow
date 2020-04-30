using BeerOverflow.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.DTOs;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerOverflowUnitTest.Service.MappersTests
{
    [TestClass]
    public class ReviewMappersTests
    {
        [TestMethod]
        public void MapReviewToDTO_ShouldReturnBreweryDTO()
        {
            //Arrange
            var cut = new Review();
            //Act
            var sut = cut.MapReviewToDTO();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(ReviewDTO));
        }

        [TestMethod]
        public void MapReviewToDTO_ShouldReturnCorrectNameAndCountry()
        {
            //Arrange
            var cut = new Review
            {
                Description = "Great",
                Beer = new Beer() { Name = "Carlsberg" },
                User = new User() { Name = "SuperMan"}
            };
            //Act
            var sut = cut.MapReviewToDTO();
            //Assert
            Assert.AreEqual(sut.Beer.Name, "Carlsberg");
            Assert.AreEqual(sut.User.Name, "SuperMan");
            Assert.AreEqual(sut.Description, "Great");

        }

        [TestMethod]
        public void MapReviewToDTO_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new Review
            {
                ID = 1,
                Description = "Great",
                Beer = new Beer() { Name = "Carlsberg" },
                User = new User() { Name = "SuperMan" }
            };
            //Act
            var sut = cut.MapReviewToDTO();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapReviewToDTO_ShouldReturnEmptyDTOIfFailed()
        {
            //Arrange
            //Act
            var sut = ReviewMapper.MapReviewToDTO(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Description, null);
            Assert.AreEqual(sut.Beer, null);
            Assert.AreEqual(sut.User, null);
        }

        [TestMethod]
        public void MapDTOToReview_ShouldReturnBrewery()
        {
            //Arrange
            var cut = new ReviewDTO
            {
                ID = 1,
                Description = "Great",
                Beer = new BeerDTO() { Name = "Carlsberg" },
                User = new UserDTO() { Name = "SuperMan" }
            };
            //Act
            var sut = cut.MapDTOToReview();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(Review));
        }

        [TestMethod]
        public void MapDTOToReview_ShouldReturnCorrectNameAndCountry()
        {
            //Arrange
            var cut = new ReviewDTO
            {
                ID = 1,
                Description = "Great",
                Beer = new BeerDTO() { Name = "Carlsberg" },
                User = new UserDTO() { Name = "SuperMan" }
            };
            //Act
            var sut = cut.MapDTOToReview();
            //Assert
            Assert.AreEqual(sut.Beer.Name, "Carlsberg");
            Assert.AreEqual(sut.User.Name, "SuperMan");
            Assert.AreEqual(sut.Description, "Great");
        }

        [TestMethod]
        public void MapDTOToReview_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new ReviewDTO
            {
                ID = 1,
                Description = "Great",
                Beer = new BeerDTO() { Name = "Carlsberg" },
                User = new UserDTO() { Name = "SuperMan" }
            };
            //Act
            var sut = cut.MapDTOToReview();
            //Assert
            Assert.AreEqual(1,sut.ID);
        }

        [TestMethod]
        public void MapDTOToReview_ShouldReturnEmptyCountryIfFailed()
        {
            //Arrange
            //Act
            var sut = ReviewMapper.MapDTOToReview(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Beer, null);
            Assert.AreEqual(sut.Comments, null);
            Assert.AreEqual(sut.User, null);
            Assert.AreEqual(sut.CreatedOn, default);
            Assert.AreEqual(sut.ModifiedOn, null);
            Assert.AreEqual(sut.DeletedOn, null);
            Assert.AreEqual(sut.IsDeleted, false);
        }
    }
}
