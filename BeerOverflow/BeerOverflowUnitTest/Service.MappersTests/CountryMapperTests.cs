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
    public class CountryMapperTests
    {
        [TestMethod]
        public void MapCountryToDTO_ShouldReturnCountryDTO()
        {
            //Arrange
            var cut = new Country();
            //Act
            var sut = cut.MapCountryToDTO();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(CountryDTO));
        }

        [TestMethod]
        public void MapCountryToDTO_ShouldReturnCorrectName()
        {
            //Arrange
            var cut = new Country
            {
                Name = "Bulgaria"
            };
            //Act
            var sut = cut.MapCountryToDTO();
            //Assert
            Assert.AreEqual(sut.Name, "Bulgaria");
        }

        [TestMethod]
        public void MapCountryToDTO_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new Country
            {
                ID = 1
            };
            //Act
            var sut = cut.MapCountryToDTO();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapCountryToDTO_ShouldReturnEmptyDTOIfFailed()
        {
            //Arrange
            //Act
            var sut = CountryMapper.MapCountryToDTO(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.Breweries, null);
        }

        [TestMethod]
        public void MapDTOToCountry_ShouldReturnCountry()
        {
            //Arrange
            var cut = new CountryDTO();
            //Act
            var sut = cut.MapDTOToCountry();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(Country));
        }

        [TestMethod]
        public void MapDTOToCountry_ShouldReturnCorrectName()
        {
            //Arrange
            var cut = new CountryDTO
            {
                Name = "Bulgaria"
            };
            //Act
            var sut = cut.MapDTOToCountry();
            //Assert
            Assert.AreEqual(sut.Name, "Bulgaria");
        }

        [TestMethod]
        public void MapDTOToCountry_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new CountryDTO
            {
                ID = 1
            };
            //Act
            var sut = cut.MapDTOToCountry();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapDTOToCountry_ShouldReturnEmptyCountryIfFailed()
        {
            //Arrange
            //Act
            var sut = CountryMapper.MapDTOToCountry(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.Breweries, null);
            Assert.AreEqual(sut.CreatedOn, default);
            Assert.AreEqual(sut.ModifiedOn, null);
            Assert.AreEqual(sut.DeletedOn, null);
            Assert.AreEqual(sut.IsDeleted, false);
        }
    }
}
