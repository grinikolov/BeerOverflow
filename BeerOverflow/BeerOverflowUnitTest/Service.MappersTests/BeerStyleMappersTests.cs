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
    public class BeerStyleMappersTests
    {
        [TestMethod]
        public void MapStyleToDTO_ShouldReturnBeerStyleDTO()
        {
            //Arrange
            var cut = new BeerStyle();
            //Act
            var sut = cut.MapStyleToDTO();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(BeerStyleDTO));
        }

        [TestMethod]
        public void MapStyleToDTO_ShouldReturnCorrectNameAndDescription()
        {
            //Arrange
            var cut = new BeerStyle()
            {
                Name = "Ale",
                Description = "This Description"
            };
            //Act
            var sut = cut.MapStyleToDTO();
            //Assert
            Assert.AreEqual(sut.Name, "Ale");
            Assert.AreEqual(sut.Description, "This Description");
        }

        [TestMethod]
        public void MapStyleToDTO_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new BeerStyle()
            {
                ID = 1
            };
            //Act
            var sut = cut.MapStyleToDTO();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapStyleToDTO_ShouldReturnEmptyDTOIfFailed()
        {
            //Arrange
            //Act
            var sut = BeerStyleMapper.MapStyleToDTO(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.Description, null);
        }

        [TestMethod]
        public void MapDTOToStyle_ShouldReturnBeerStyle()
        {
            //Arrange
            var cut = new BeerStyleDTO();
            //Act
            var sut = cut.MapDTOToStyle();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(BeerStyle));
        }

        [TestMethod]
        public void MapDTOToStyle_ShouldReturnCorrectNameAndDescription()
        {
            //Arrange
            var cut = new BeerStyleDTO()
            {
                Name = "Ale",
                Description = "This Description"
            };
            //Act
            var sut = cut.MapDTOToStyle();
            //Assert
            Assert.AreEqual(sut.Name, "Ale");
            Assert.AreEqual(sut.Description, "This Description");
        }

        [TestMethod]
        public void MapDTOToStyle_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new BeerStyleDTO()
            {
                ID = 1
            };
            //Act
            var sut = cut.MapDTOToStyle();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapDTOToStyle_ShouldReturnEmptyBeerStyleIfFailed()
        {
            //Arrange
            //Act
            var sut = BeerStyleMapper.MapDTOToStyle(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.Description, null);
            Assert.AreEqual(sut.CreatedOn, default);
            Assert.AreEqual(sut.ModifiedOn, null);
            Assert.AreEqual(sut.DeletedOn, null);
            Assert.AreEqual(sut.IsDeleted, false);
        }
    }
}
