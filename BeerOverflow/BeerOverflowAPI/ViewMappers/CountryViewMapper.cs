using BeerOverflowAPI.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Mappers
{
    public static class CountryViewMapper
    {
        public static CountryDTO MapCountryViewToDTO(this CountryViewModel view)
        {
            var countryDTO = new CountryDTO() {
                ID = view.ID,
                Name = view.Name,
                IsDeleted = view.IsDeleted
            };
            if (view.ID !=null)
            {
                countryDTO.ID = view.ID;
            }
            if (view.Breweries != null)
            {
                countryDTO.Breweries = view.Breweries.Select(n => new BreweryDTO() { Name = n }).ToList();
            }
            return countryDTO;
        }

        public static CountryViewModel MapCountryDTOToView(this CountryDTO dto)
        {
            var countryView = new CountryViewModel()
            {
                ID = dto.ID,
                Name = dto.Name,
                IsDeleted = dto.IsDeleted
            };
            if (dto.Breweries != null)
            {
                countryView.Breweries = dto.Breweries.Select(b => b.Name).ToList();
            }
            return countryView;
        }
    }
}
