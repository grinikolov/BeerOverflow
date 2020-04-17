using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IBeerService
    {
        BeerDTO Create(BeerDTO breweryDTO);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        IEnumerable<BeerDTO> GetAll();
        BeerDTO GetBeer(int id);
        BeerDTO Update(int id, BeerDTO breweryDTO);
    }
}
