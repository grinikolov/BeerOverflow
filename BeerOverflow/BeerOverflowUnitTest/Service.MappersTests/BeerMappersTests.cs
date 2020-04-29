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
    public class BeerMappersTests
    {
        [TestMethod]
        public void MapBeerToDTO_ShouldReturnBreweryDTO()
        {
            //Arrange
            var cut = new Beer();
            //Act
            var sut = cut.MapBeerToDTO();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(BeerDTO));
        }

        [TestMethod]
        public void MapBeerToDTO_ShouldReturnCorrectNameAndCountryAndBreweryAndStyleAndABVAndRating()
        {
            //Arrange
            var cut = new Beer
            {
                ABV = 4.5f,
                Rating = 2,
                Name = "Carlsberg",
                Country = new Country() { Name = "Bulgaria" },
                Brewery = new Brewery() { Name = "Brewery", Country = new Country() { Name = "Germany" } },
                Style = new BeerStyle() { Name = "Ale" }


            };
            //Act
            var sut = cut.MapBeerToDTO();
            //Assert
            Assert.AreEqual(sut.Name, "Carlsberg");
            Assert.AreEqual(sut.Country.Name, "Bulgaria");
            Assert.AreEqual(sut.Brewery.Name, "Brewery");
            Assert.AreEqual(sut.Brewery.Country, "Germany");
            Assert.AreEqual(sut.Style.Name, "Ale");
            Assert.AreEqual(sut.ABV, 4.5f);
            Assert.AreEqual(sut.Rating, 2);
        }

        [TestMethod]
        public void MapBeerToDTO_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new Beer
            {
                ID = 1,
                ABV = 4.5f,
                Rating = 2,
                Name = "Carlsberg",
                Country = new Country() { Name = "Bulgaria" },
                Brewery = new Brewery() { Name = "Brewery", Country = new Country() { Name = "Germany" } },
                Style = new BeerStyle() { Name = "Ale" }


            };
            //Act
            var sut = cut.MapBeerToDTO();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapBeerToDTO_ShouldReturnEmptyDTOIfFailed()
        {
            //Arrange
            //Act
            var sut = BeerMapper.MapBeerToDTO(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.ABV, default);
            Assert.AreEqual(sut.Rating, default);
            Assert.AreEqual(sut.Country, null);
            Assert.AreEqual(sut.Brewery, null);
            Assert.AreEqual(sut.Style, null);
            Assert.AreEqual(sut.Reviews, null);
        }

        [TestMethod]
        public void MapDTOToBeer_ShouldReturnBrewery()
        {
            //Arrange
            var cut = new BeerDTO
            {
                ID = 1,
                ABV = 4.5f,
                Rating = 2,
                Name = "Carlsberg",
                Country = new CountryDTO() { Name = "Bulgaria" },
                Brewery = new BreweryDTO() { Name = "Brewery", Country = "Germany" },
                Style = new BeerStyleDTO() { Name = "Ale" }


            };
            //Act
            var sut = cut.MapDTOToBeer();
            //Assert
            Assert.IsInstanceOfType(sut, typeof(Beer));
        }

        [TestMethod]
        public void MapDTOToBeer_ShouldReturnCorrectNameAndCountryAndBreweryAndStyle()
        {
            //Arrange
            var cut = new BeerDTO
            {
                ABV = 4.5f,
                Rating = 2,
                Name = "Carlsberg",
                Country = new CountryDTO() { Name = "Bulgaria" },
                Brewery = new BreweryDTO() { Name = "Brewery", Country = "Germany" },
                Style = new BeerStyleDTO() { Name = "Ale" }


            };
            //Act
            var sut = cut.MapDTOToBeer();
            //Assert
            Assert.AreEqual(sut.Name, "Carlsberg");
            Assert.AreEqual(sut.Country.Name, "Bulgaria");
            Assert.AreEqual(sut.Brewery.Name, "Brewery");
            Assert.AreEqual(sut.Brewery.Country.Name, "Germany");
            Assert.AreEqual(sut.Style.Name, "Ale");
            Assert.AreEqual(sut.ABV, 4.5f);
            Assert.AreEqual(sut.Rating, 2);
        }

        [TestMethod]
        public void MapDTOToBeer_ShouldReturnCorrectID()
        {
            //Arrange
            var cut = new BeerDTO
            {
                ID = 1,
                ABV = 4.5f,
                Rating = 2,
                Name = "Carlsberg",
                Country = new CountryDTO() { Name = "Bulgaria" },
                Brewery = new BreweryDTO() { Name = "Brewery", Country = "Germany" },
                Style = new BeerStyleDTO() { Name = "Ale" }


            };
            //Act
            var sut = cut.MapDTOToBeer();
            //Assert
            Assert.AreEqual(sut.ID, 1);
        }

        [TestMethod]
        public void MapDTOToBeer_ShouldReturnEmptyCountryIfFailed()
        {
            //Arrange
            //Act
            var sut = BeerMapper.MapDTOToBeer(null);
            //Assert
            Assert.AreEqual(sut.ID, null);
            Assert.AreEqual(sut.Name, null);
            Assert.AreEqual(sut.ABV, default);
            Assert.AreEqual(sut.Rating, default);
            Assert.AreEqual(sut.Country, null);
            Assert.AreEqual(sut.Brewery, null);
            Assert.AreEqual(sut.Style, null);
            Assert.AreEqual(sut.Reviews, null);
            Assert.AreEqual(sut.CreatedOn, default);
            Assert.AreEqual(sut.ModifiedOn, null);
            Assert.AreEqual(sut.DeletedOn, null);
            Assert.AreEqual(sut.IsDeleted, false);
        }
    }
}
