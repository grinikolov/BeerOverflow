using BeerOverflow.Models;
using Services.Contracts;
using Services.DTOs;
using System;
using System.Collections.Generic;

namespace Services
{
    public class BeerStyleService : IBeerStyleService
    {
        public BeerStyleDTO CreateStyle(BeerStyleDTO style)
        {
            BeerStyle theNewStyleDTO = new BeerStyle
            {
                //TODO: use DTO here
                ID = style.ID,
                Name = style.Name,
                Description = style.Description
            };
            // TODO: Database add: 
            //Database.BeerStyles.Add(theNewStyleDTO);

            return style;
        }

        public bool DeleteStyle()
        {
            throw new NotImplementedException();
        }

        public ICollection<BeerStyleDTO> GetAllStyles()
        {
            throw new NotImplementedException();
        }

        public BeerStyleDTO GetStyle(int id)
        {
            throw new NotImplementedException();
        }

        public BeerStyleDTO UpdateStyle(int id, string newName, string newDescription)
        {
            throw new NotImplementedException();
        }
    }
}
