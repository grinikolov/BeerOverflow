using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICountriesService
    {
        CountryDTO Create(CountryDTO model);
        Task<CountryDTO> CreateAsync(CountryDTO model);
        bool Delete(int id);
        Task<bool> DeleteAsync(int id);
        CountryDTO Get(int id);
        IEnumerable<CountryDTO> GetAll();
        CountryDTO Update(int id, CountryDTO model);
    }
}
