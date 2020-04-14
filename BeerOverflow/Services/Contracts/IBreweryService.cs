using BeerOverflow.Models;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBreweryService
    {
        BreweryDTO Create(BreweryDTO breweryDTO);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        IEnumerable<BreweryDTO> GetAll();
        BreweryDTO GetBrewery(int id);
        BreweryDTO Update(int id, BreweryDTO breweryDTO);

    }
}
