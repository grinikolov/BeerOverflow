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

    public class CountriesService : ICountriesService
    {
        private readonly BOContext _context;

        public CountriesService(BOContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<CountryDTO>> GetAll()
        {
            var countries = await this._context.Countries.Select(c => 
            new CountryDTO()
            {
                ID = c.ID,
                Name = c.Name,
                Breweries = c.Breweries.Select(b => new BreweryDTO()
                {
                    ID = b.ID,
                    Name = b.Name,
                    Country = b.Country.Name,
                    Beers = b.Beers.Select(b => new BeerDTO()
                    {
                        ID = b.ID,
                        Name = b.Name,
                        //Country = b.Country.Name
                        ABV = b.ABV,
                        Style = new BeerStyleDTO()
                        {
                            ID = b.Style.ID,
                            Name = b.Style.Name,
                            Description = b.Style.Description
                        },
                    }).ToList()
                }).ToList(),
            }).ToListAsync();
            return countries;
        }


        public async Task<CountryDTO> GetAsync(int id)
        {
            var country = await this._context.Countries.FindAsync(id);
            try
            {
                if (country == null)
                {
                    throw new ArgumentNullException();
                }
            }
            catch (Exception)
            {

                return null;
            }

            //TODO: convert Brewery to BreweryDTO
            var model = new CountryDTO
            {
                ID = country.ID,
                Name = country.Name,
                Breweries = country.Breweries.Select(b => new BreweryDTO()
                {
                    ID = b.ID,
                    Name = b.Name,
                    Country = b.Country.Name,
                    Beers = b.Beers.Select(b => new BeerDTO()
                    {
                        ID = b.ID,
                        Name = b.Name,
                        //Country = b.Country.Name
                        ABV = b.ABV,
                        Style = new BeerStyleDTO()
                        {
                            ID = b.Style.ID,
                            Name = b.Style.Name,
                            Description = b.Style.Description
                        },
                    }).ToList()
                }).ToList(),
            };

            return model;
        }

        //public CountryDTO Create(CountryDTO model)
        //{
        //    var country = new Country
        //    {
        //        //id to be set by DB itself
        //        Name = model.Name,
        //        //Breweries = model.Breweries,
        //        Breweries = new List<Brewery>(),
        //        CreatedOn = DateTime.UtcNow,
        //    };
        //    //TODO: check if such Country already exists, then do not add it

        //    var theCountry = this._context.Countries
        //        .Where(c => c.IsDeleted == false)
        //        .FirstOrDefault(c => c.Name == model.Name);

        //    if (theCountry == null)
        //    {
        //        this._context.Countries.Add(country);
        //        this._context.SaveChanges();

        //        theCountry = this._context.Countries
        //        .Where(c => c.IsDeleted == false)
        //        .FirstOrDefault(c => c.Name == model.Name);
        //    }

        //    var countryToReturn = new CountryDTO()
        //    {
        //        ID = theCountry.ID,
        //        Name = theCountry.Name,
        //        //Breweries = theCountry.Breweries.Select(b => new BreweryDTO()
        //        //{
        //        //    ID = b.ID,
        //        //    Name = b.Name,
        //        //    Country = b.Country.Name,
        //        //}).ToList()
        //    };

        //    return countryToReturn;
        //}
        public async Task<CountryDTO> CreateAsync(CountryDTO model)
        {
            var country = new Country
            {
                //ID to be set by DB itself
                Name = model.Name,
                // Breweries = new List<Brewery>(),
                CreatedOn = DateTime.UtcNow,
            };

            #region Check if exists
            var theCountry = await this._context.Countries
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Name == model.Name);

            if (theCountry == null)
            {
                await this._context.Countries.AddAsync(country);
                await this._context.SaveChangesAsync();
            }
            #endregion
            model.ID = this._context.Countries
                .FirstOrDefault(c => c.Name == model.Name).ID;
            //var countryToReturn = new CountryDTO()
            //{
            //    ID = theCountry.ID,
            //    Name = theCountry.Name,
            //    //Breweries = theCountry.Breweries.Select(b => new BreweryDTO()
            //    //{
            //    //    ID = b.ID,
            //    //    Name = b.Name,
            //    //    Country = b.Country.Name,
            //    //}).ToList()
            //};

            return model;
        }
        /// <summary>
        /// Updates the Country's Name
        /// </summary>
        /// <param name="id">ID of the Country to be updated.</param>
        /// <param name="model">Provide model's Name=newName.</param>
        /// <returns></returns>
        public async Task<CountryDTO> UpdateAsync(int id, CountryDTO model)
        {
            //if (id != model.ID)
            //{
            //    throw new ArgumentNullException();
            //}

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

        //public bool Delete(int id)
        //{
        //    try
        //    {
        //        var country = this._context.Countries.Find(id) ?? throw new ArgumentNullException("Country not found.");
        //        country.IsDeleted = true;
        //        country.DeletedOn = DateTime.UtcNow;
        //        //this._context.Countries.Remove(country);
        //        this._context.Update(country);
        //        this._context.SaveChanges();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var country = await this._context.Countries.FindAsync(id)
                    ?? throw new ArgumentNullException("Country not found.");
                country.IsDeleted = true;
                country.ModifiedOn = country.DeletedOn = DateTime.UtcNow;

                // TODO: Delete 

                foreach (var brew in country.Breweries)
                {
                    var newBreweryService = new BreweryServices(this._context);
                    await newBreweryService.Delete(brew.ID);
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


        private bool CountryExists(int id)
        {
            return this._context.Countries.Any(c => c.ID == id);
        }
    }
}
