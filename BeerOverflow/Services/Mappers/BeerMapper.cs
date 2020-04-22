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
                //TODO: The beer's Country should not be null:
                Country = new CountryDTO() { Name = beer.Country.Name },
                Style = new BeerStyleDTO() { Name = beer.Style.Name, Description = beer.Style.Description },
                Brewery = new BreweryDTO() { Name = beer.Brewery.Name, Country = beer.Brewery.Country.Name },
                Reviews = beer.Reviews.Select(r => new ReviewDTO() 
                    { 
                    })
                .ToList()
            };

                return beerDTO;
        }
    }
}
