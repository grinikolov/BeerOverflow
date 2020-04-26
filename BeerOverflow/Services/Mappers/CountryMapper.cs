using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mappers
{
    public static class CountryMapper
    {
        public static CountryDTO MapCountryToDTO(this Country country)
        {
            try
            {
                //TODO: Test with actual list of brewery
                var countryDTO = new CountryDTO()
                {
                    ID = country.ID,
                    Name = country.Name,
                };
                if (country.Breweries != null)
                {
                    countryDTO.Breweries = country.Breweries.Select(n => n.MapBreweryToDTO()).ToList();
                }
                else
                {
                    countryDTO.Breweries = null;
                }
                return countryDTO;
            }
            catch (Exception)
            {

                return new CountryDTO();
            }
        }

        public static Country MapDTOToCountry(this CountryDTO dto)
        {
            try
            {
                var country = new Country()
                {
                    ID = dto.ID,
                    Name = dto.Name,
                    
                };
                if (dto.Breweries != null)
                {
                    country.Breweries = dto.Breweries.Select(n => n.MapDTOToBrewery()).ToList();
                }
                else
                {
                    country.Breweries = null;
                }
                return country;
            }
            catch (Exception)
            {

                return new Country();
            }

        }
    }
}
