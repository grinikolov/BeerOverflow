using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBeerService
    {
        Task<BeerDTO> CreateAsync(BeerDTO breweryDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<BeerDTO>> GetAllAsync();
        Task<BeerDTO> GetBeerAsync(int id);
        Task<BeerDTO> UpdateAsync(int id, BeerDTO breweryDTO);
        Task<IEnumerable<BeerDTO>> Filter(string filteringName, string filteringStyle, string filteringCountry);
        Task<IEnumerable<BeerDTO>> Search(string searchString);
        Task<BeerDTO> GetRandom();
    }
}
