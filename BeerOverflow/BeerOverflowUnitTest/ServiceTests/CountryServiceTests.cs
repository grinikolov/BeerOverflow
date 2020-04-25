using BeerOverflow.Models;
using Database;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services;
using Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowUnitTest
{
    [TestClass]
    public class CountryServiceTests
    {
        [TestMethod]
        public async Task GetAll_ShouldReturnEmptyIfNoCountriesAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAll_ShouldReturnEmptyIfNoCountriesAsync");
            using (var context = new BOContext(options))
            {

            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAll();
                //Assert
                Assert.AreEqual(result.Count(), 0);
            }
        }

        [TestMethod]
        public async Task GetAll_ShouldReturnIEnumerableCountryDTOAsync()
        {
            //Arrange
            var options = InMemory.GetOptions("GetAll_ShouldReturnIEnumerableCountryDTOAsync");
            using (var context = new BOContext(options))
            {
                var country = new Country()
                {
                    Name = "Bulgaria"
                };
                context.Countries.Add(country);
                await context.SaveChangesAsync();
            }

            using (var context = new BOContext(options))
            {
                //Act
                var sut = new CountriesService(context);
                var result = await sut.GetAll();
                //Assert
                Assert.IsInstanceOfType(result, typeof(IEnumerable<CountryDTO>));;
            }
        }
    }
}
