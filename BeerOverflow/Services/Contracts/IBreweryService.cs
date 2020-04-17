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
        Task<BreweryDTO> Create(BreweryDTO breweryDTO);
        Task<bool> Delete(int id);
        Task<IEnumerable<BreweryDTO>> GetAll();
        Task<BreweryDTO> GetBrewery(int id);
        Task<BreweryDTO> Update(int id, BreweryDTO breweryDTO);

    }
}
