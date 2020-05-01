using BeerOverflowAPI.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.ViewMappers
{
    public static class BreweryViewMapper
    {
        public static BreweryDTO MapBreweryViewToDTO(this BreweryViewModel view)
        {
            var breweryDTO = new BreweryDTO()
            {
                ID = view.ID,
                Name = view.Name,
                CountryID = view.CountryID,
                IsDeleted = view.IsDeleted
            };
            if (view.ID != null)
            {
                breweryDTO.ID = view.ID;
            }
            if (view.Beers != null)
            {
                breweryDTO.Beers = view.Beers.Select(b => new BeerDTO { Name = b }).ToList();
            }
            return breweryDTO;
        }

        public static BreweryViewModel MapBreweryDTOToView(this BreweryDTO dto)
        {
            var breweryView = new BreweryViewModel()
            {
                ID = dto.ID,
                Name = dto.Name,
                CountryID = dto.CountryID,
                Country = dto.Country,
                IsDeleted = dto.IsDeleted
            };
            if (dto.Beers != null)
            {
                breweryView.Beers = dto.Beers.Select(b => b.Name).ToList();
            }
            return breweryView;
        }
    }
}
