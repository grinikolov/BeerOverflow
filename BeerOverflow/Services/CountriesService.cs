using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services.DTOs;
using Services.Contracts;

namespace Services
{

    public class CountriesService : ICountriesService, ICountriesService1
    {
        private readonly BOContext _context;

        public CountriesService(BOContext context)
        {
            this._context = context;
        }

        public IEnumerable<CountryDTO> GetAll()
        {
            //TODO: convert Brewery to BreweryDTO
            var countries = this._context.Countries
                .Select(c => new CountryDTO
                {
                    ID = c.ID,
                    Name = c.Name,
                    //Breweries = c.Breweries,
                })
                .ToList();
            return countries;
        }


        public CountryDTO Get(int id)
        {
            var country = this._context.Countries.Find(id);

            if (country == null)
            {
                throw new ArgumentNullException();
            }
            //TODO: convert Brewery to BreweryDTO
            var model = new CountryDTO
            {
                ID = country.ID,
                Name = country.Name,
                //Breweries = country.Breweries,
            };

            return model;
        }

        public CountryDTO Create(CountryDTO model)
        {
            var country = new Country
            {
                //id to be set by DB itself
                Name = model.Name,
                //Breweries = model.Breweries,
                Breweries = new List<Brewery>(),
                CreatedOn = DateTime.UtcNow,
            };
            //TODO: check if such style already exists, then do not add it
            this._context.Countries.Add(country);
            this._context.SaveChanges();

            return model;
        }
        public async Task<CountryDTO> CreateAsync(CountryDTO model)
        {
            var country = new Country
            {
                //ID to be set by DB itself
                Name = model.Name,
                //Breweries = model.Breweries,
                Breweries = new List<Brewery>(),
                CreatedOn = DateTime.UtcNow,
            };
            //TODO: check if such style already exists, then do not add it
            await this._context.Countries.AddAsync(country);
            await this._context.SaveChangesAsync();

            return model;
        }
        /// <summary>
        /// Updates the Country's Name
        /// </summary>
        /// <param name="id">ID of the Country to be updated.</param>
        /// <param name="model">Provide model's Name=newName.</param>
        /// <returns></returns>
        public CountryDTO Update(int id, CountryDTO model)
        {
            if (id != model.ID)
            {
                throw new ArgumentNullException();
            }

            var country = this._context.Countries.Find(id) ?? throw new ArgumentNullException("The country is not found");
            country.Name = model.Name;

            country.ModifiedOn = DateTime.UtcNow;

            this._context.Update(country);
            try
            {
                this._context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return model;
                }
                else
                {
                    throw;
                }
            }

            return model;
        }

        public bool Delete(int id)
        {
            try
            {
                var country = this._context.Countries.Find(id) ?? throw new ArgumentNullException("Country not found.");
                country.IsDeleted = true;
                country.DeletedOn = DateTime.UtcNow;
                //this._context.Countries.Remove(country);
                this._context.Update(country);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var country = await this._context.Countries.FindAsync(id) ?? throw new ArgumentNullException("Country not found.");
                country.IsDeleted = true;
                country.DeletedOn = DateTime.UtcNow;
                //this._context.Countries.Remove(country);
                this._context.Update(country);
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool CountryExists(int id)
        {
            return this._context.Countries.Any(c => c.ID == id);
        }
    }
}
