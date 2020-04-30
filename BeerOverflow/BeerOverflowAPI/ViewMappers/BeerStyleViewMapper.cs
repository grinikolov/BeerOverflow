using BeerOverflowAPI.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerOverflowAPI.ViewMappers
{
    public static class BeerStyleViewMapper
    {
        public static BeerStyleViewModel MapDTOToView(this BeerStyleDTO styleDTO)
        {
            try
            {
                //TODO: Test with actual list of brewery
                var style = new BeerStyleViewModel()
                {
                    ID = styleDTO.ID,
                    Name = styleDTO.Name,
                    Description = styleDTO.Description
                };
                return style;
            }
            catch (Exception)
            {

                return new BeerStyleViewModel();
            }
        }
        public static BeerStyleDTO MapViewToDTO(this BeerStyleViewModel style)
        {
            try
            {
                //TODO: Test with actual list of brewery
                var styleDTO = new BeerStyleDTO()
                {
                    ID = style.ID == 0? 0:style.ID,
                    Name = style.Name,
                    Description = style.Description
                };
                return styleDTO;
            }
            catch (Exception)
            {

                return new BeerStyleDTO();
            }
        }




    }
}
