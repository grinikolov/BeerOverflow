using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services.DTOs;
using Services.Contracts;
using Services.Mappers;

namespace Services
{

    public class CountriesService : ICountriesService
    {
        private readonly BOContext _context;

        public CountriesService(BOContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get all countries on record
        /// </summary>
        /// <returns>Returns a modified list of countries on record</returns>
        public async Task<IEnumerable<CountryDTO>> GetAllAsync()
        {
            var countries = await this._context.Countries.Include(c => c.Breweries).ToListAsync();
            var countriesDTO = countries.Select(c => c.MapCountryToDTO()).ToList();
            if (countriesDTO.Any(c => c.Name == null))
            {
                return null;
            }
            return countriesDTO;
        }

        /// <summary>
        /// Gets a country by ID
        /// </summary>
        /// <param name="id">Id of country</param>
        /// <returns>Returns a modified specific country on record</returns>
        public async Task<CountryDTO> GetAsync(int? id)
        {
            var country = await this._context.Countries.Include(c => c.Breweries).FirstOrDefaultAsync(c => c.ID == id);

            if (country == null)
            {
                return null;
            }
            var model = country.MapCountryToDTO();
            if (model.Name == null)
            {
                return null;
            }
            return model;
        }
        /// <summary>
        /// Creates a country and writes it to the database.
        /// </summary>
        /// <param name="model">Input CountryDTO object</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<CountryDTO> CreateAsync(CountryDTO model)
        {
            var country = model.MapDTOToCountry();
            if (country.Name == null)
            {
                return null;
            }
            #region Check if exists
            var theCountry = await this._context.Countries
                .FirstOrDefaultAsync(c => c.Name == model.Name);

            if (theCountry == null)
            {
                country.CreatedOn = DateTime.UtcNow;
                await this._context.Countries.AddAsync(country);
                await this._context.SaveChangesAsync();
            }
            else
            {
                theCountry.IsDeleted = false;
                theCountry.DeletedOn = null;
                theCountry.ModifiedOn = DateTime.UtcNow;
                _context.Countries.Update(theCountry);
                var breweriesOfCountry = await _context.Breweries.Where(b => b.CountryID == theCountry.ID).ToListAsync();
                foreach (var item in breweriesOfCountry)
                {
                    await new BreweryServices(_context).CreateAsync(item.MapBreweryToDTO());
                }
                await this._context.SaveChangesAsync();
            }
            #endregion
            model.ID = this._context.Countries
                .FirstOrDefault(c => c.Name == model.Name).ID;
            return model;
        }

        /// <summary>
        /// Updates the Country's Name
        /// </summary>
        /// <param name="id">ID of the Country to be updated.</param>
        /// <param name="model">Input object with update information.</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<CountryDTO> UpdateAsync(int? id, CountryDTO model)
        {

            var country = await this._context.Countries.FindAsync(id);
            if (country == null) return null;
            country.Name = model.Name;
            country.ModifiedOn = DateTime.UtcNow;
            model.ID = country.ID;

            this._context.Update(country);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return null;
                }
            }
            return model;
        }

        /// <summary>
        /// Deletes specified record of country
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns>Bool</returns>
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var country = await this._context.Countries.Include(b => b.Breweries).FirstAsync(c => c.ID == id)
                    ?? throw new ArgumentNullException("Country not found.");
                country.IsDeleted = true;
                country.ModifiedOn = country.DeletedOn = DateTime.UtcNow;

                foreach (var brew in country.Breweries)
                {
                    var newBreweryService = new BreweryServices(this._context);
                    await newBreweryService.DeleteAsync(brew.ID);
                }

                this._context.Update(country);

                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // for MVC only
        public async Task<bool> RecoverAsync(int id)
        {
            try
            {
                var country = await this._context.Countries.Include(b => b.Breweries).FirstAsync(c => c.ID == id)
                    ?? throw new ArgumentNullException("Country not found.");
                country.IsDeleted = false;
                country.ModifiedOn = DateTime.UtcNow;
                country.DeletedOn = null;
                // TODO: Delete 

                foreach (var brew in country.Breweries)
                {
                    var newBreweryService = new BreweryServices(this._context);
                    await newBreweryService.DeleteAsync(brew.ID);
                }

                this._context.Update(country);

                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CountryExists(int? id)
        {
            return this._context.Countries.Any(c => c.ID == id);
        }
    }
}
