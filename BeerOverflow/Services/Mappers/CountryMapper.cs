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
            var countryDTO = new CountryDTO()
            {
                ID = country.ID,
                Name = country.Name,
                Breweries = country.Breweries.Select(n => n.MapBreweryToDTO()).ToList()
            };
            return countryDTO;
        }

        public static Country MapDTOToCountry(this CountryDTO dto)
        {
            var country = new Country()
            {
                ID = dto.ID,
                Name = dto.Name,
                Breweries = dto.Breweries.Select(n => n.MapDTOToBrewery()).ToList()
            };
            return country;
        }
    }
}
