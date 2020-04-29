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
                    Country = beer.Country.MapCountryToDTO(),
                    Style = beer.Style.MapStyleToDTO(),
                    Brewery = beer.Brewery.MapBreweryToDTO(),  
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
                    Country = dto.Country.MapDTOToCountry(),
                    Style = dto.Style.MapDTOToStyle(),
                    Brewery = dto.Brewery.MapDTOToBrewery()
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
