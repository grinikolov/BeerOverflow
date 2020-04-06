using BeerOverflow.Models;
using Services.Contracts;
using System;
using System.Collections.Generic;

namespace Services
{
    public class BeerStyleService : IBeerStyleService
    {
        public BeerStyle CreateStyle(BeerStyle style)
        {
            var newStyle = new BeerStyle
            {
                ID = style.ID,
                Name = style.Name,
                Description = style.Description,
                CreatedOn = style.CreatedOn,
                ModifiedOn = style.ModifiedOn,
                DeletedOn = style.DeletedOn,
                IsDeleted = style.IsDeleted,
            };
            throw new NotImplementedException();
        }

        public bool DeleteStyle()
        {
            throw new NotImplementedException();
        }

        public ICollection<BeerStyle> GetAllStyles()
        {
            throw new NotImplementedException();
        }

        public BeerStyle GetStyle(Guid id)
        {
            throw new NotImplementedException();
        }

        public BeerStyle UpdateStyle(Guid id, string newName, string newDescription)
        {
            throw new NotImplementedException();
        }
    }
}
