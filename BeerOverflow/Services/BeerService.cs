﻿using BeerOverflow.Models;
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
        public async Task<BeerDTO> CreateAsync(BeerDTO model)
        {
            var beer = new Beer
            {
                Name = model.Name,
                Country = this._context.Countries.FirstOrDefault(c => c.Name == model.Country.Name),
                Style = this._context.BeerStyles.FirstOrDefault(s => s.Name == model.Style.Name),
                Brewery = this._context.Breweries.FirstOrDefault(b => b.Name == model.Brewery.Name),
                CreatedOn = DateTime.UtcNow,
                Rating = default,
            };
            #region Check if exists
            var theBeer = await this._context.Beers
                .Where(c => c.IsDeleted == false)
                .FirstOrDefaultAsync(c => c.Name == model.Name);

            if (theBeer == null)
            {
                await this._context.Beers.AddAsync(beer);
                await this._context.SaveChangesAsync();
            }
            #endregion
            model.ID = this._context.Beers
                .FirstOrDefault(c => c.Name == model.Name).ID;
            return model;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var beer = await _context.Beers.FindAsync(id)
                    ?? throw new ArgumentNullException();
                beer.IsDeleted = true;
                beer.DeletedOn = beer.ModifiedOn = DateTime.UtcNow;
                _context.Beers.Update(beer);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

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
        public async Task<IEnumerable<BeerDTO>> GetAllAsync()
        {
            var beers = await this._context.Beers
                .Include(c => c.Country)
                .Include(b => b.Reviews)
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                    .ThenInclude(brew => brew.Country)
                .Where(br => br.IsDeleted == false)
                .Select(b=> b.MapBeerToDTO())
                //.Select(b => new BeerDTO
                //{
                //    ID = b.ID,
                //    Name = b.Name,
                //    Rating = b.Rating,
                //    Country = new CountryDTO() { Name = b.Country.Name },
                //    Style = new BeerStyleDTO() { Name = b.Style.Name, Description = b.Style.Description },
                //    Brewery = new BreweryDTO() { Name = b.Brewery.Name, Country = b.Brewery.Country.Name },
                //    Reviews = b.Reviews.Select(r => new ReviewDTO() { }).ToList()
                //})
                .ToListAsync();
            return beers;
        }

        public async Task<BeerDTO> GetBeerAsync(int id)
        {
            var beer = await this._context.Beers
                .Include(b => b.Country)
                .Include(b => b.Reviews)
                .Include(b => b.Style)
                .Include(b => b.Brewery)
                    .ThenInclude(brew => brew.Country)
                .Where(br => br.IsDeleted == false).FirstOrDefaultAsync(br => br.ID == id);

            if (beer == null)
            {
                return null;
            }

            var beerDTO = beer.MapBeerToDTO();
            //var beerDTO = new BeerDTO
            //{
            //    ID = beer.ID,
            //    Name = beer.Name,
            //    Rating = beer.Rating,
            //    Country = new CountryDTO() { Name = beer.Country.Name },
            //    Style = new BeerStyleDTO() { Name = beer.Style.Name, Description = beer.Style.Description },
            //    Brewery = new BreweryDTO() { Name = beer.Brewery.Name, Country = beer.Brewery.Country.Name },
            //    Reviews = beer.Reviews.Select(r => new ReviewDTO() { }).ToList()
            //};

            return beerDTO;
        }

        public async Task<BeerDTO> UpdateAsync(int id, BeerDTO beerDTO)
        {
            var beer = await _context.Beers.Include(b => b.Brewery).FirstOrDefaultAsync(b => b.ID == id);
            beer.Name = beerDTO.Name;
            beer.Country = _context.Countries.FirstOrDefault(c => c.Name == beerDTO.Country.Name);
            beer.Style = _context.BeerStyles.Find(beerDTO.Style.ID);
            beer.Brewery = _context.Breweries.Find(beerDTO.Brewery.ID);
            beer.ModifiedOn = DateTime.UtcNow;
            beerDTO.ID = beer.ID;
            _context.Beers.Update(beer);
            await this._context.SaveChangesAsync();

            return beerDTO;
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
