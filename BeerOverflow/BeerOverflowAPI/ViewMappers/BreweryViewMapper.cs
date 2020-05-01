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
                //ID = view.ID,
                Name = view.Name,
                CountryID = view.CountryID
                //Breweries = view.Breweries.Select(n => new BreweryDTO() { Name = n}).ToList()
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
                Country = dto.Country
                //Breweries = dto.Breweries.Select(n => n.Name).ToList()
            };
            if (dto.Beers != null)
            {
                breweryView.Beers = dto.Beers.Select(b => b.Name).ToList();
            }
            return breweryView;
        }
    }
}
