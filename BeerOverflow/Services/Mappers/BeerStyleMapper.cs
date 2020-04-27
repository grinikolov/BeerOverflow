using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mappers
{
    public static class BeerStyleMapper
    {
        public static BeerStyleDTO MapStyleToDTO(this BeerStyle style)
        {
            try
            {
                //TODO: Test with actual list of brewery
                var styleDTO = new BeerStyleDTO()
                {
                    ID = style.ID,
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

        public static BeerStyle MapDTOToStyle(this BeerStyleDTO dto)
        {
            try
            {
                var style = new BeerStyle()
                {
                    ID = dto.ID,
                    Name = dto.Name,
                    Description = dto.Description
                };
                return style;
            }
            catch (Exception)
            {

                return new BeerStyle();
            }
        }
    }
}
