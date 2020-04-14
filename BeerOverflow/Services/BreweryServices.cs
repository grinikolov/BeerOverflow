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
        public BreweryDTO Create(BreweryDTO breweryDTO)
        {
            var brewery = new Brewery
            {
                ID = breweryDTO.ID,
                Name = breweryDTO.Name,
                Country = _context.Countries.FirstOrDefault(c => c.Name == breweryDTO.Country),
                CreatedOn = DateTime.UtcNow,
            };

            _context.Breweries.AddAsync(brewery);
            this._context.SaveChangesAsync();

            return breweryDTO;
        }

        public bool Delete(int id)
        {
            try
            {
                var brewery = _context.Breweries.Find(id);
                brewery.IsDeleted = true;
                brewery.DeletedOn = brewery.ModifiedOn = DateTime.UtcNow;
                _context.Breweries.Update(brewery);
                //brewery.DeletedOn = DateTime.UtcNow;
                //this._context.BeerStyles.Remove(beerStyle);
                _context.SaveChanges();

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
                var brewery = await _context.Breweries.FindAsync(id);
                brewery.IsDeleted = true;
                brewery.DeletedOn = brewery.ModifiedOn = DateTime.UtcNow;
                _context.Breweries.Update(brewery);
                //brewery.DeletedOn = DateTime.UtcNow;
                //this._context.BeerStyles.Remove(beerStyle);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<BreweryDTO> GetAll()
        {
            var breweries = this._context.Breweries.Include(b => b.Country)
                .Where(br => br.IsDeleted == false)
                .Select(b => new BreweryDTO
                {
                    ID = b.ID,
                    Name = b.Name,
                    Country = b.Country.Name
                })
                .ToList();
            return breweries;
        }

        public BreweryDTO GetBrewery(int id)
        {
            var breweries = this._context.Breweries.Include(b => b.CountryID)
                .Where(br => br.IsDeleted == false).FirstOrDefault(br => br.ID == id);
            //.Select(b => new BreweryDTO
            //{
            //    ID = b.ID,
            //    Name = b.Name,
            //    Country = b.Country.Name
            //})
            //.ToList();
            //        var theBeerStyle = _context.BeerStyles
            //.Where(style => style.IsDeleted == false)
            //.FirstOrDefault(style => style.ID == id);

            if (breweries == null)
            {
                throw new ArgumentNullException();
            }
            //convert to dto
            var breweriesDTO = new BreweryDTO
            {
                ID = breweries.ID,
                Name = breweries.Name,
                Country = breweries.Country.Name,
            };

            return breweriesDTO;
        }

        public BreweryDTO Update(int id, BreweryDTO breweryDTO)
        {
            var brewery = _context.Breweries.Find(id);
            brewery.Name = breweryDTO.Name;
            brewery.Country = _context.Countries.FirstOrDefault(c => c.Name == breweryDTO.Country);
            brewery.ModifiedOn = DateTime.UtcNow;
            _context.Breweries.Update(brewery);
            //var brewery = new Brewery
            //{
            //    ID = breweryDTO.ID,
            //    Name = breweryDTO.Name,
            //    Country = _context.Countries.FirstOrDefault(c => c.Name == breweryDTO.Country),
            //    CreatedOn = DateTime.UtcNow,
            //};

            //_context.Breweries.AddAsync(brewery);
            this._context.SaveChangesAsync();

            return breweryDTO;
        }
    }
}
