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
            var cut = new Country
            {
                Breweries = new List<Brewery>()
            };
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
                Name = "Bulgaria",
                Breweries = new List<Brewery>()
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
                ID = 1,
                Breweries = new List<Brewery>()
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
            var cut = new Country();
            //Act
            var sut = cut.MapCountryToDTO();
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.Breweries, null);
        }
    }
}
