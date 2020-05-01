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
                    CountryID = brewery.CountryID
                };
                if (brewery.Beers != null)
                {
                    breweryDTO.Beers = brewery.Beers.Select(b => b.MapBeerToDTO()).ToList();
                }
                else
                {
                    breweryDTO.Beers = null;
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
            try
            {
                var brewery = new Brewery()
                {
                    ID = dto.ID,
                    Name = dto.Name,
                    CountryID = dto.CountryID
                    //TODO: Country should not be created anew
                    //Country = new Country() { Name = dto.Country },
                };
                if (dto.Beers != null)
                {
                    brewery.Beers = dto.Beers.Select(b => b.MapDTOToBeer()).ToList();
                }
                else
                {
                    brewery.Beers = null;
                }
                return brewery;
            }
            catch (Exception)
            {

                return new Brewery();
            }

        }
    }
}
