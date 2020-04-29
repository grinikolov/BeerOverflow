using Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IBeerStylesService
    {
        Task<BeerStyleDTO> CreateAsync(BeerStyleDTO beerStyleDTO);
        Task<bool> DeleteAsync(int? id);
        Task<IEnumerable<BeerStyleDTO>> GetAllAsync();
        Task<BeerStyleDTO> GetAsync(int? id);
        Task<BeerStyleDTO> UpdateAsync(int? id, BeerStyleDTO beerStyleDTO);
    }
}