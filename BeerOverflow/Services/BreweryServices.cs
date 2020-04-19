using BeerOverflow.Models;
using Database;
using Microsoft.EntityFrameworkCore;
using Services.Contracts;
using Services.DTOs;
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
        public async Task<BreweryDTO> Create(BreweryDTO breweryDTO)
        {
            var brewery = new Brewery
            {
                Name = breweryDTO.Name,
                Country = _context.Countries.FirstOrDefault(c =>
                        c.Name == breweryDTO.Country),

                CreatedOn = DateTime.UtcNow,
            };


            #region Check if exists
            var theBrewery = await this._context.Breweries
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.Name == breweryDTO.Name);

            if (theBrewery == null)
            {
                await _context.Breweries.AddAsync(brewery);
                await this._context.SaveChangesAsync();
            }
            #endregion

            breweryDTO.ID = this._context.Breweries
                .FirstOrDefault(b => b.Name == breweryDTO.Name).ID;

            return breweryDTO;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var brewery = await _context.Breweries.FindAsync(id);
                brewery.IsDeleted = true;
                brewery.DeletedOn = brewery.ModifiedOn = DateTime.UtcNow;
                _context.Breweries.Update(brewery);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<BreweryDTO>> GetAll()
        {
            var breweries = await this._context.Breweries
                .Include(b => b.Country)
                .Where(br => br.IsDeleted == false)
                .Select(br =>  new BreweryDTO()
            {
                ID = br.ID,
                Name = br.Name,
                Country = br.Country.Name,
                Beers = br.Beers.Select(b => new BeerDTO()
                {
                    ID = b.ID,
                    Name = b.Name,
                    ABV = b.ABV,
                    Style = new BeerStyleDTO()
                    {
                        ID = b.Style.ID,
                        Name = b.Style.Name,
                        Description = b.Style.Description
                    },
                }).ToList()
            }).ToListAsync();

            return breweries;
        }

        public async Task<BreweryDTO> GetBrewery(int id)
        {
            var brewery = await this._context.Breweries
                .Include(b => b.Country).Include(b => b.Beers)
                .Where(br => br.IsDeleted == false).FirstOrDefaultAsync(br => br.ID == id);

            if (brewery == null)
            {
                return null;
            }
            var breweryDTO = new BreweryDTO()
            {
                ID = brewery.ID,
                Name = brewery.Name,
                Country = brewery.Country.Name,
                Beers = brewery.Beers.Select(b => new BeerDTO()
                {
                    ID = b.ID,
                    Name = b.Name,
                    ABV = b.ABV,
                    Style = new BeerStyleDTO()
                    {
                        ID = b.Style.ID,
                        Name = b.Style.Name,
                        Description = b.Style.Description
                    },
                }).ToList()
            };

            return breweryDTO;
        }

        public async Task<BreweryDTO> Update(int id, BreweryDTO breweryDTO)
        {
            var brewery = await _context.Breweries.Include(b => b.Beers).FirstOrDefaultAsync(b => b.ID == id);
            brewery.Name = breweryDTO.Name;
            brewery.Country = _context.Countries.FirstOrDefault(c =>
                    c.Name == breweryDTO.Country);
            brewery.ModifiedOn = DateTime.UtcNow;
            _context.Breweries.Update(brewery);
            breweryDTO.ID = brewery.ID;
            breweryDTO.Beers = brewery.Beers.Select(b => new BeerDTO()
            {
                ID = b.ID,
                Name = b.Name,
                ABV = b.ABV,
                Style = new BeerStyleDTO()
                {
                    ID = b.Style.ID,
                    Name = b.Style.Name,
                    Description = b.Style.Description
                }
            }).ToList();         
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
            return breweryDTO;
        }


        private bool BreweryExists(int id)
        {
            return this._context.Breweries.Any(b => b.ID == id);
        }

    }
}
