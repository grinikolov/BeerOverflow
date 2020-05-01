using BeerOverflowAPI.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.ViewMappers
{
    public static class BeerViewMapper
    {
        public static BeerViewModel MapBeerDTOToView(this BeerDTO dto)
        {
            var beerView = new BeerViewModel()
            {
                ID = dto.ID,
                Name = dto.Name,
                Style = dto.Style.Name,
                Country = dto.Country.Name,
                Brewery = dto.Brewery.Name,
                Rating = dto.Rating,
                ABV = dto.ABV,
            };
            if (dto.Reviews != null)
            {
                beerView.Reviews = dto.Reviews.Select(r => r.Description).ToList();
            }
            return beerView;
        }
    }
}
