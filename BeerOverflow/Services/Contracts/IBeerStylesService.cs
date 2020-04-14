using Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IBeerStylesService
    {
        BeerStyleDTO Create(BeerStyleDTO beerStyleDTO);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        IEnumerable<BeerStyleDTO> GetAll();
        BeerStyleDTO Get(int id);
        BeerStyleDTO Update(int id, BeerStyleDTO beerStyleDTO);
    }
}