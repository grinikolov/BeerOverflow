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
                beerView.Reviews = dto.Reviews.Select(r => r.MapDTOToReviewView()).ToList();
            }
            return beerView;
        }

        public static BeerDTO MapBeerViewToDTO(this BeerViewModel view)
        {
            var dto = new BeerDTO()
            {
                ID = view.ID,
                Name = view.Name,
                StyleID = view.StyleID,
                //Style = view.Style.Name,
                CountryID = view.CountryID,
                //Country = view.Country.Name,
                BreweryID = view.BreweryID,
                //Brewery = view.Brewery.Name,
                Rating = view.Rating,
                ABV = view.ABV,
            };
            if (view.Reviews != null)
            {
               dto.Reviews = view.Reviews.Select(r => r.MapReviewViewToDTO()).ToList();
            }
            return dto;
        }


    }
}
