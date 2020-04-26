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
            try
            {
                var breweryDTO = new BreweryDTO()
                {
                    ID = brewery.ID,
                    Name = brewery.Name,
                    Country = brewery.Country.Name,
                };
                if (brewery.Beers != null)
                {
                    breweryDTO.Beers = brewery.Beers.Select(b => b.MapBeerToDTO()).ToList();
                }
                else
                {
                    breweryDTO.Beers = new List<BeerDTO>();
                }
                return breweryDTO;
            }
            catch (Exception)
            {

                return new BreweryDTO();
            }
            
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
