using BeerOverflow.Models;
using Database;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services.DTOs;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BreweryServices : IBreweryService
    {
        private readonly BOContext _context;

        public BreweryServices(BOContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get all breweried on record
        /// </summary>
        /// <returns>Returns a modified list of breweries on record</returns>
        public async Task<IEnumerable<BreweryDTO>> GetAllAsync()
        {
            var breweries = await this._context.Breweries.Include(b => b.Country).Include(b => b.Beers).ToListAsync();
            var breweriesDTO = breweries.Select(b => b.MapBreweryToDTO()).ToList();
            if (breweriesDTO.Any(c => c.Name == null))
            {
                return null;
            }
            return breweriesDTO;
        }

        /// <summary>
        /// Gets a brewery by ID
        /// </summary>
        /// <param name="id">Id of brewery</param>
        /// <returns>Returns a modified specific brewery on record</returns>
        public async Task<BreweryDTO> GetAsync(int? id)
        {
            var brewery = await this._context.Breweries
                .Include(b => b.Country)
                .Include(b => b.Beers)
                .FirstOrDefaultAsync(br => br.ID == id);

            if (brewery == null)
            {
                return null;
            }
            var model = brewery.MapBreweryToDTO();

            return model;
        }

        /// <summary>
        /// Creates a brewery and writes it to the database.
        /// </summary>
        /// <param name="model">Input BreweryDTO object</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<BreweryDTO> CreateAsync(BreweryDTO model)
        {
            var brewery = model.MapDTOToBrewery();
            if (brewery.Name == null)
            {
                return null;
            }
            #region Check if exists
            var theBrewery = await this._context.Breweries
                .FirstOrDefaultAsync(b => b.Name == model.Name);

            if (theBrewery == null)
            {
                brewery.CreatedOn = DateTime.UtcNow;
                await _context.Breweries.AddAsync(brewery);
                await this._context.SaveChangesAsync();
            }
            else
            {
                theBrewery.IsDeleted = false;
                theBrewery.DeletedOn = null;
                theBrewery.ModifiedOn = DateTime.UtcNow;
                _context.Breweries.Update(theBrewery);
                var beersOfBrewery = await _context.Beers
                    .Include(b => b.Country)
                    .Include(b => b.Brewery)
                    .Include(b => b.Style)
                    .Include(b => b.Reviews)
                    .Where(b => b.BreweryID == theBrewery.ID)
                    .Where(b => b.Style.IsDeleted == false)
                    .ToListAsync();
                foreach (var item in beersOfBrewery)
                {
                    await new BeerService(_context).CreateAsync(item.MapBeerToDTO());
                }
                await this._context.SaveChangesAsync();
            }
            #endregion
            var returnModel = await this._context.Breweries
                .FirstOrDefaultAsync(b => b.Name == model.Name);
            model.ID = returnModel.ID;
            return model;
        }

        /// <summary>
        /// Updates the Brewery's Name and Country
        /// </summary>
        /// <param name="id">ID of the Brewery to be updated.</param>
        /// <param name="model">Input object with update information</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<BreweryDTO> UpdateAsync(int? id, BreweryDTO model)
        {
            var brewery = await _context.Breweries.Include(b => b.Country).Include(b => b.Beers).FirstOrDefaultAsync(b => b.ID == id);
            if (brewery == null) return null;
            brewery.Name = model.Name;
            brewery.Country = await _context.Countries.FirstOrDefaultAsync(c =>
                    c.Name == model.Country);
            brewery.ModifiedOn = DateTime.UtcNow;
            _context.Breweries.Update(brewery);
            model.ID = brewery.ID;      
            
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
                {
                    return null;
                }
            }
            return model;
        }

        /// <summary>
        /// Deletes specified record of brewery
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns>Bool</returns>
        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                var brewery = await _context.Breweries.Include(b => b.Beers).FirstAsync(b => b.ID == id)
                    ?? throw new ArgumentNullException("Brewery not found.");
                brewery.IsDeleted = true;
                brewery.DeletedOn = brewery.ModifiedOn = DateTime.UtcNow;

                foreach (var beer in brewery.Beers)
                {
                    var newBeerService = new BeerService(this._context);
                    await newBeerService.DeleteAsync(beer.ID);
                }
                _context.Breweries.Update(brewery);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private bool BreweryExists(int? id)
        {
            return this._context.Breweries.Any(b => b.ID == id);
        }

    }
}
