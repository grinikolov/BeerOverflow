using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Contracts
{
    public interface IBeerStyleService
    {
        BeerStyleDTO GetStyle(int id);
        ICollection<BeerStyleDTO> GetAllStyles();
        BeerStyleDTO CreateStyle(BeerStyleDTO style);
        BeerStyleDTO UpdateStyle(int id, string newName, string newDescription);
        bool DeleteStyle();
    }
}
