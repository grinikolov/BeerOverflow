using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services.DTOs;
using Services.Mappers;

namespace Services
{

    public class BeerStylesService : IBeerStylesService
    {
        private readonly BOContext _context;

        public BeerStylesService(BOContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get all styles on record
        /// </summary>
        /// <returns>Returns a modified list of countries on record</returns>
        public async Task<IEnumerable<BeerStyleDTO>> GetAllAsync()
        {
            var beerStyles = await this._context.BeerStyles.ToListAsync();
            var beerStylesDTO = beerStyles.Select(style => style.MapStyleToDTO()).ToList();
            if (beerStylesDTO.Any(c => c.Name == null))
            {
                return null;
            }
            return beerStylesDTO;
        }

        /// <summary>
        /// Gets a style by ID
        /// </summary>
        /// <param name="id">Id of style</param>
        /// <returns>Returns a modified specific style on record</returns>
        public async Task<BeerStyleDTO> GetAsync(int? id)
        {
            var theBeerStyle = await  _context.BeerStyles.FirstOrDefaultAsync(style => style.ID == id);

            if (theBeerStyle == null)
            {
                return null;
            }
            var model = theBeerStyle.MapStyleToDTO();
            if (model.Name == null)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// Creates a style and writes it to the database.
        /// </summary>
        /// <param name="model">Input BeerStyleDTO object</param>
        /// <returns>Returns the re-evaluated input object</returns>
        public async Task<BeerStyleDTO> CreateAsync(BeerStyleDTO model)
        {
            var beerStyle = model.MapDTOToStyle();
            if (beerStyle.Name == null)
            {
                return null;
            }
            #region Check if exists
            var theStyle = this._context.BeerStyles
                .FirstOrDefault(b => b.Name == model.Name);

            if (theStyle == null)
            {
                beerStyle.CreatedOn = DateTime.UtcNow;
                await this._context.BeerStyles.AddAsync(beerStyle);
                await this._context.SaveChangesAsync();
            }
            else
            {
                theStyle.IsDeleted = false;
                theStyle.DeletedOn = null;
                theStyle.ModifiedOn = DateTime.UtcNow;
                _context.BeerStyles.Update(theStyle);
                var beersOfBeerStyle = await _context.Beers
                    .Include(b => b.Country)
                    .Include(b => b.Brewery)
                    .Include(b => b.Style)
                    .Include(b => b.Reviews)
                    .Where(b => b.StyleID == theStyle.ID).
                    Where(b => b.Brewery.IsDeleted == false).ToListAsync();
                foreach (var item in beersOfBeerStyle)
                {
                    await new BeerService(_context).CreateAsync(item.MapBeerToDTO());
                }
                await this._context.SaveChangesAsync();
            }
            #endregion
            var returnModel = await this._context.BeerStyles
                .FirstOrDefaultAsync(b => b.Name == model.Name);
            model.ID = returnModel.ID;
            return model;
        }

        /// <summary>
        /// Updates the Style's Name and Description
        /// </summary>
        /// <param name="id">ID of the Style to be updated.</param>
        /// <param name="model">Input object with update information.</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<BeerStyleDTO> UpdateAsync(int? id, BeerStyleDTO model)
        {
            var beerStyle = await this._context.BeerStyles.FindAsync(id);
            if (beerStyle == null) return null;
            beerStyle.Name = model.Name;
            beerStyle.Description = model.Description;
            beerStyle.ModifiedOn = DateTime.UtcNow;
            model.ID = id;

            this._context.Update(beerStyle);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerStyleExists(id))
                {
                    return null;
                }
            }
            return model;
        }

        /// <summary>
        /// Deletes specified record of beer style
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns>Bool</returns>
        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                var beerStyle = await this._context.BeerStyles.FindAsync(id)
                    ?? throw new ArgumentNullException("Style not found.");
                beerStyle.IsDeleted = true;
                beerStyle.ModifiedOn = beerStyle.DeletedOn = DateTime.UtcNow;
                var beersOfStyle = await _context.Beers.ToListAsync();
                foreach (var beer in beersOfStyle)
                {
                    var newBeerService = new BeerService(this._context);
                    await newBeerService.DeleteAsync(beer.ID);
                }
                this._context.Update(beerStyle);
                await this._context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool BeerStyleExists(int? id)
        {
            return this._context.BeerStyles.Any(bs => bs.ID == id);
        }
    }
}
