using BeerOverflow.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IBeerStyleService
    {
        BeerStyle GetStyle(Guid id);
        ICollection<BeerStyle> GetAllStyles();
        BeerStyle CreateStyle(BeerStyle style);
        BeerStyle UpdateStyle(Guid id, string newName, string newDescription);
        bool DeleteStyle();
    }
}
