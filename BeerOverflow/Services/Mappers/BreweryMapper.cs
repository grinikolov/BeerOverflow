using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mappers
{
    public static class BreweryMapper
    {
        public static BreweryDTO MapBreweryToDTO(this Brewery brewery)
        {
            var breweryDTO = new BreweryDTO()
            {
                ID = brewery.ID,
                Name = brewery.Name,
                Country = brewery.Country.Name,
                Beers = brewery.Beers.Select(b => b.MapBeerToDTO()).ToList()// new BeerDTO()
                //{
                //    ID = b.ID,
                //    Name = b.Name,
                //    ABV = b.ABV,
                //    Style = new BeerStyleDTO()
                //    {
                //        ID = b.Style.ID,
                //        Name = b.Style.Name,
                //        Description = b.Style.Description
                //    },
                //}).ToList()
            };
            return breweryDTO;
        }

        public static Brewery MapDTOToBrewery(this BreweryDTO dto)
        {
            var brewery = new Brewery()
            {
                ID = dto.ID,
                Name = dto.Name,
                //Country = 
                Beers = dto.Beers.Select(b => b.MapDTOToBeer()).ToList()
            };
            return brewery;
        }
    }
}
