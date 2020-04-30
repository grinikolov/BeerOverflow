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
    public class BeerService : IBeerService
    {
        private readonly BOContext _context;

        public BeerService(BOContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get all beers on record
        /// </summary>
        /// <returns>Returns a modified list of beers on record</returns>
        public async Task<IEnumerable<BeerDTO>> GetAllAsync()
        {
            var beers = await this._context.Beers
                .Include(b => b.Country)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.User)
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                    .ThenInclude(brew => brew.Country)
                .ToListAsync();
            var beersDTO = beers.Select(b => b.MapBeerToDTO()).ToList();
            if (beersDTO.Any(c => c.Name == null))
            {
                return null;
            }
            return beersDTO;
        }

        /// <summary>
        /// Gets a beer by ID
        /// </summary>
        /// <param name="id">Id of beer</param>
        /// <returns>Returns a modified specific beer on record</returns>
        public async Task<BeerDTO> GetAsync(int? id)
        {
            var beer = await this._context.Beers
                .Include(b => b.Country)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.User)
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                    .ThenInclude(brew => brew.Country)
                .FirstOrDefaultAsync(br => br.ID == id);

            if (beer == null)
            {
                return null;
            }

            var model = beer.MapBeerToDTO();
            if (model.Name == null)
            {
                return null;
            }
            return model;
        }

        /// <summary>
        /// Creates a beer and writes it to the database.
        /// </summary>
        /// <param name="model">Input BeerDTO object</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<BeerDTO> CreateAsync(BeerDTO model)
        {
            var beer = model.MapDTOToBeer();
            if (beer.Name == null)
            {
                return null;
            }
            #region Check if exists
            var theBeer = await this._context.Beers
                .FirstOrDefaultAsync(b => b.Name == model.Name);

            if (theBeer == null)
            {
                beer.CreatedOn = DateTime.UtcNow;
                await this._context.Beers.AddAsync(beer);
                await this._context.SaveChangesAsync();
            }
            else
            {
                theBeer.IsDeleted = false;
                theBeer.DeletedOn = null;
                theBeer.ModifiedOn = DateTime.UtcNow;
                _context.Beers.Update(theBeer);
                var reviewsOfBeer = await _context.Reviews
                    .Include(r => r.Beer)
                    .Include(r => r.User)
                    .Include(r =>r.Comments)
                    .Where(r => r.BeerID == theBeer.ID).ToListAsync();
                foreach (var item in reviewsOfBeer)
                {
                    await new ReviewsService(_context).CreateAsync(item.MapReviewToDTO());
                }
                var wishListOfBeer = await _context.WishLists
                                                .Where(w => w.BeerID == theBeer.ID)
                                                .ToListAsync();
                foreach (var item in wishListOfBeer)
                {
                    _context.WishLists.Remove(item);
                }
                var drinkListOfBeer = await _context.DrankLists
                                                .Where(d => d.BeerID == theBeer.ID)
                                                .ToListAsync();
                foreach (var item in drinkListOfBeer)
                {
                    _context.DrankLists.Remove(item);
                }
                await this._context.SaveChangesAsync();
            }
            #endregion
            var returnModel = await this._context.Beers
                .FirstOrDefaultAsync(c => c.Name == model.Name);
            model.ID = returnModel.ID;
            return model;
        }
        /// <summary>
        /// Updates the Beer's Name, Style, Brewery and Country
        /// </summary>
        /// <param name="id">ID of the Beer to be updated.</param>
        /// <param name="model">Input object with update information.</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<BeerDTO> UpdateAsync(int? id, BeerDTO model)
        {
            var beer = await this._context.Beers
               .Include(b => b.Country)
               .Include(b => b.Style)
               .Include(b => b.Brewery)
               .FirstOrDefaultAsync(br => br.ID == id);
            if (beer == null) return null;
            beer.ABV = model.ABV;
            beer.Name = model.Name;
            beer.Country = _context.Countries.FirstOrDefault(c => c.Name == model.Country.Name);
            beer.Style = _context.BeerStyles.FirstOrDefault(s => s.Name == model.Style.Name);
            beer.Brewery = _context.Breweries.FirstOrDefault(b => b.Name == model.Brewery.Name);
            beer.ModifiedOn = DateTime.UtcNow;
            model.ID = beer.ID;
            _context.Beers.Update(beer);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
                {
                    return null;
                }
            }
            return model;
        }

        /// <summary>
        /// Deletes specified record of beer
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns>Bool</returns>
        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                var beer = await this._context.Beers
                .Include(b => b.Country)
                .Include(b => b.Reviews)
                    .ThenInclude(r => r.User)
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                    .ThenInclude(brew => brew.Country)
                .Include(b => b.DrankLists)
                .Include(b => b.WishLists)
                .FirstOrDefaultAsync(b => b.ID == id)
                    ?? throw new ArgumentNullException();
                beer.IsDeleted = true;
                beer.DeletedOn = beer.ModifiedOn = DateTime.UtcNow;
                foreach (var item in beer.Reviews)
                {
                    await new ReviewsService(_context).DeleteAsync(item.ID);
                }
                foreach (var item in beer.WishLists)
                {
                    _context.WishLists.Remove(item);
                }
                foreach (var item in beer.DrankLists)
                {
                    _context.DrankLists.Remove(item);
                }
                _context.Beers.Update(beer);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        private bool BeerExists(int? id)
        {
            return this._context.Beers.Any(c => c.ID == id);
        }

        //TODO: Not tested
        //TODO: Not finished in the API controller
        public async Task<IEnumerable<BeerDTO>> Filter(string filterName, string filterStyle, string filterCountry)
        {
            var beersQuery = this._context.Beers
                .Include(c => c.Country)
                .Include(b => b.Reviews)
                .Where(br => br.IsDeleted == false).AsQueryable();

            if (filterName != null)
            {
                beersQuery = beersQuery.Where(b => b.Name.ToLower().Contains(filterName));
            }
            if (filterCountry != null)
            {
                beersQuery = beersQuery.Where(b => b.Country.Name.ToLower().Contains(filterCountry));
            }
            if (filterStyle != null)
            {
                beersQuery = beersQuery.Where(b => b.Style.Name.ToLower().Contains(filterStyle));
            }
            var beersToReturn = await beersQuery.Select(b => b.MapBeerToDTO()).ToListAsync();

            return beersToReturn;
        }
        public async Task<IEnumerable<BeerDTO>> Search(string searchString)//, string breweryName, string countryName)
        {

            var beers = await this._context.Beers
                .Include(b => b.Brewery)
                    .ThenInclude(b => b.Country)
                .Include(b => b.Country)
                .Include(b => b.Style)
                .Include(b => b.Reviews)
                .Where(b => b.Name.ToLower().Contains(searchString.ToLower())
                    || b.Country.Name.ToLower().Contains(searchString.ToLower())
                    || b.Brewery.Name.ToLower().Contains(searchString.ToLower())
                ).ToListAsync();

            var beersToReturn = beers.Select(b => b.MapBeerToDTO()).ToList();
            return beersToReturn;
        }


        public async Task<BeerDTO> GetRandom()
        {
            int count = await this._context.Beers.CountAsync();
            Random random = new Random();
            int id = random.Next(1, count + 1);
            var beer = await this._context.Beers
                .Include(b => b.Country)
                .Include(b => b.Reviews)
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                .Where(br => br.IsDeleted == false)
                .FirstOrDefaultAsync(br => br.ID == id);

            if (beer == null)
            {
                return null;
            }
            var beerDTO = beer.MapBeerToDTO();

            return beerDTO;
        }

    }
}
