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
            var beerDTO = new BeerDTO
            {
                ID = beer.ID,
                Name = beer.Name,
                //TODO: The beer's Country should not be null: .Include it
                Country = new CountryDTO() { Name = beer.Country.Name },
                Style = new BeerStyleDTO() { Name = beer.Style.Name, Description = beer.Style.Description },
                Brewery = new BreweryDTO() { Name = beer.Brewery.Name, Country = beer.Brewery.Country.Name },
                Reviews = beer.Reviews.Select(r => r.MapReviewToDTO())
                .ToList()
            };

                return beerDTO;
        }

        public static Beer MapDTOToBeer(this BeerDTO dto)
        {
            var beer = new Beer
            {
                ID = dto.ID,
                Name = dto.Name,
                //TODO: The beer's Country should not be null: .Include it
                //Country = new CountryDTO() { Name = beer.Country.Name },
                //Style = new BeerStyleDTO() { Name = beer.Style.Name, Description = beer.Style.Description },
                //Brewery = new BreweryDTO() { Name = beer.Brewery.Name, Country = beer.Brewery.Country.Name },
                //Reviews = beer.Reviews.Select(r => r.MapReviewToDTO())
                //.ToList()
            };

            return beer;
        }
    }
}
