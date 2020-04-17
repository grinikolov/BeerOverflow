using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICountriesService
    {
        //CountryDTO Create(CountryDTO model);
        Task<CountryDTO> CreateAsync(CountryDTO model);
        //bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        Task<CountryDTO> GetAsync(int id);
        Task<IEnumerable<CountryDTO>> GetAll();
        Task<CountryDTO> UpdateAsync(int id, CountryDTO model);
    }
}
