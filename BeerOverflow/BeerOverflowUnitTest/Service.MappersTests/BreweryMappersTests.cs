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
    public class BreweryMappersTests
    {
        [TestMethod]
        public void MapBreweryToDTO_ShouldReturnBreweryDTO()
        {
            //Arrange
            var cut = new Brewery();
            //Act
            var sut = cut.MapBreweryToDTO();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(BreweryDTO));
        }

        [TestMethod]
        public void MapBreweryToDTO_ShouldReturnCorrectNameAndCountry()
        {
            //Arrange
            var cut = new Brewery
            {
                Name = "Brew1",
                Country = new Country() { Name = "Bulgaria"}
            };
            //Act
            var sut = cut.MapBreweryToDTO();
            //Assert
            Assert.AreEqual(sut.Name, "Brew1");
            Assert.AreEqual(sut.Country, "Bulgaria");
        }

        [TestMethod]
        public void MapBreweryToDTO_ShouldReturnCorrectID()
        {
            //Arrange
            var but = new Brewery
            {
                ID = 1,
                Country = new Country()
            };
            //Act
            var sut = but.MapBreweryToDTO();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapBreweryToDTO_ShouldReturnEmptyDTOIfFailed()
        {
            //Arrange
            //Act
            var sut = BreweryMapper.MapBreweryToDTO(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.Beers, null);
            Assert.AreEqual(sut.Country, null);
        }

        [TestMethod]
        public void MapDTOToBrewery_ShouldReturnBrewery()
        {
            //Arrange
            var cut = new BreweryDTO() { Name = "Bulgaria"};
            //Act
            var sut = cut.MapDTOToBrewery();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(Brewery));
        }

        [TestMethod]
        public void MapDTOToBrewery_ShouldReturnCorrectNameAndCountry()
        {
            //Arrange
            var cut = new BreweryDTO
            {
                Name = "Brewery",
                Country = "Bulgaria"
            };
            //Act
            var sut = cut.MapDTOToBrewery();
            //Assert
            Assert.AreEqual(sut.Name, "Brewery");
            Assert.AreEqual(sut.Country.Name, "Bulgaria");
        }

        [TestMethod]
        public void MapDTOToBrewery_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new BreweryDTO
            {
                ID = 1,
                Country = "Bulgaria"
            };
            //Act
            var sut = cut.MapDTOToBrewery();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapDTOToBrewery_ShouldReturnEmptyCountryIfFailed()
        {
            //Arrange
            //Act
            var sut = BreweryMapper.MapDTOToBrewery(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.Country, null);
            Assert.AreEqual(sut.CountryID, default);
            Assert.AreEqual(sut.Beers, null);
            Assert.AreEqual(sut.CreatedOn, default);
            Assert.AreEqual(sut.ModifiedOn, null);
            Assert.AreEqual(sut.DeletedOn, null);
            Assert.AreEqual(sut.IsDeleted, false);
        }
    }
}
