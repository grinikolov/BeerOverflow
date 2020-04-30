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
    public class CommentMappersTestscs
    {
        [TestMethod]
        public void MapCopmmentToDTO_ShouldReturnCountryDTO()
        {
            //Arrange
            var cut = new Comment();
            //Act
            var sut = cut.MapCommentToDTO();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(CommentDTO));
        }

        [TestMethod]
        public void MapCopmmentToDTO_ShouldReturnCorrectDescription()
        {
            //Arrange
            var cut = new Comment
            {
                Description = "Gotham",
                User = new User() { Name = "Batman"},
                Review = new Review() { Description = "Description"}
            };
            //Act
            var sut = cut.MapCommentToDTO();
            //Assert
            Assert.AreEqual(sut.Description, "Gotham");
        }

        [TestMethod]
        public void MapCopmmentToDTO_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new Comment
            {
                ID = 1,
                Description = "Gotham",
                User = new User() { Name = "Batman" },
                Review = new Review() { Description = "Description" }
            };
            //Act
            var sut = cut.MapCommentToDTO();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapCopmmentToDTO_ShouldReturnEmptyDTOIfFailed()
        {
            //Arrange
            //Act
            var sut = CommentMapper.MapCommentToDTO(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Description, null);
            Assert.AreEqual(sut.Review, null);
            Assert.AreEqual(sut.User, null);
        }

        [TestMethod]
        public void MapDTOToComment_ShouldReturnCountry()
        {
            //Arrange
            var cut = new CommentDTO();
            //Act
            var sut = cut.MapDTOToComment();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(Comment));
        }

        [TestMethod]
        public void MapDTOToComment_ShouldReturnCorrectDescription()
        {
            //Arrange
            var cut = new CommentDTO
            {
                ID = 1,
                Description = "Gotham",
                User = new UserDTO() { Name = "Batman" },
                Review = new ReviewDTO() { Description = "Description" }
            };
            //Act
            var sut = cut.MapDTOToComment();
            //Assert
            Assert.AreEqual(sut.Description, "Gotham");
        }

        [TestMethod]
        public void MapDTOToComment_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new CommentDTO
            {
                ID = 1,
                Description = "Gotham",
                User = new UserDTO() { Name = "Batman" },
                Review = new ReviewDTO() { Description = "Description" }
            };
            //Act
            var sut = cut.MapDTOToComment();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapDTOToComment_ShouldReturnEmptyCountryIfFailed()
        {
            //Arrange
            //Act
            var sut = CommentMapper.MapDTOToComment(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Description, null);
            Assert.AreEqual(sut.Review, null);
            Assert.AreEqual(sut.User, null);
            Assert.AreEqual(sut.CreatedOn, default);
            Assert.AreEqual(sut.ModifiedOn, null);
            Assert.AreEqual(sut.DeletedOn, null);
            Assert.AreEqual(sut.IsDeleted, false);
        }
    }
}
