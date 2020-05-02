using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mappers
{
    public static class BeerMapper
    {
        public static BeerDTO MapBeerToDTO(this Beer beer)
        {
            try
            {
                var beerDTO = new BeerDTO
                {
                    ID = beer.ID,
                    Name = beer.Name,
                    Rating = beer.Rating,
                    ABV = beer.ABV,
                    Country = new CountryDTO() { Name = beer.Country.Name }, //beer.Country.MapCountryToDTO(),
                    Style = new BeerStyleDTO() { Name = beer.Style.Name },//beer.Style.MapStyleToDTO(),
                    Brewery = new BreweryDTO() { Name = beer.Brewery.Name, Country = beer.Brewery.Country.Name }//beer.Brewery.MapBreweryToDTO(),  
                };
                if (beer.Reviews != null)
                {
                    beerDTO.Reviews = beer.Reviews.Select(r => r.MapReviewToDTO()).ToList();
                }
                else
                {
                    beerDTO.Reviews = null;
                }
                return beerDTO;
            }
            catch (Exception)
            {
                return new BeerDTO();
            }

        }

        public static Beer MapDTOToBeer(this BeerDTO dto)
        {
            try
            {
                var beer = new Beer()
                {
                    ID = dto.ID,
                    Name = dto.Name,
                    Rating = dto.Rating,
                    ABV = dto.ABV,
                    CountryID = dto.CountryID,
                    //Country = new Country() { Name = dto.Country.Name},//dto.Country.MapDTOToCountry(),
                    StyleID = dto.StyleID,
                    //Style = new BeerStyle() {Name = dto.Style.Name },//dto.Style.MapDTOToStyle(),
                    BreweryID = dto.BreweryID,
                    //Brewery = new Brewery() { Name = dto.Brewery.Name, Country = new Country() { Name = dto.Brewery.Country} },//dto.Brewery.MapDTOToBrewery()
                };
                if (dto.Reviews != null)
                {
                    beer.Reviews = dto.Reviews.Select(r => r.MapDTOToReview()).ToList();
                }
                else
                {
                    beer.Reviews = null;
                }
                return beer;
            }
            catch (Exception)
            {
                return new Beer();
            }
        }
    }
}
