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
        Task<BreweryDTO> CreateAsync(BreweryDTO breweryDTO);
        Task<bool> DeleteAsync(int? id);
        Task<IEnumerable<BreweryDTO>> GetAllAsync();
        Task<BreweryDTO> GetAsync(int? id);
        Task<BreweryDTO> UpdateAsync(int? id, BreweryDTO breweryDTO);

    }
}
