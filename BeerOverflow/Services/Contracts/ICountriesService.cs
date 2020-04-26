using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICountriesService
    {
        Task<CountryDTO> CreateAsync(CountryDTO model);
        Task<bool> DeleteAsync(int id);
        Task<CountryDTO> GetAsync(int? id);
        Task<IEnumerable<CountryDTO>> GetAllAsync();
        Task<CountryDTO> UpdateAsync(int? id, CountryDTO model);
    }
}
