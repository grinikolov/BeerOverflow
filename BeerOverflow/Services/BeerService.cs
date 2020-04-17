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
    public class BeerService : IBeerService
    {
        private readonly BOContext _context;

        public BeerService(BOContext context)
        {
            this._context = context;
        }
        public async Task<BeerDTO> CreateAsync(BeerDTO beerDTO)
        {
            var beer = new Beer
            {
                //ID = beerDTO.ID,
                Name = beerDTO.Name,
                Country = this._context.Countries.FirstOrDefault(c => c.Name == beerDTO.Country.Name),
                Style = this._context.BeerStyles.FirstOrDefault(s => s.Name == beerDTO.Style.Name),
                Brewery = this._context.Breweries.FirstOrDefault(b => b.Name == beerDTO.Brewery.Name),
                CreatedOn = DateTime.UtcNow,
            };

            await _context.Beers.AddAsync(beer);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
            return beerDTO;
        }

        public bool Delete(int id)
        {
            try
            {
                var beer = _context.Beers.Find(id);
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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var beer = await _context.Beers.FindAsync(id);
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

        public IEnumerable<BeerDTO> GetAll()
        {
            var beers = this._context.Beers.Include(c => c.Country).Include(b => b.Reviews)
                .Where(br => br.IsDeleted == false)
                .Select(b => new BeerDTO
                {
                    ID = b.ID,
                    Name = b.Name,
                    Country = new CountryDTO() { Name = b.Country.Name },
                    Style = new BeerStyleDTO() { Name = b.Style.Name, Description = b.Style.Description },
                    Brewery = new BreweryDTO() { Name = b.Brewery.Name, Country = b.Brewery.Country.Name },
                    Reviews = b.Reviews.Select(r => new ReviewDTO() { }).ToList()
                })
                .ToList();
            return beers;
        }

        public BeerDTO GetBeer(int id)
        {
            var beer = this._context.Beers.Include(b => b.Country).Include(b => b.Reviews)
                    .Where(br => br.IsDeleted == false).FirstOrDefault(br => br.ID == id);

            if (beer == null)
            {
                throw new ArgumentNullException();
            }
            var beerDTO = new BeerDTO
            {
                ID = beer.ID,
                Name = beer.Name,
                Country = new CountryDTO() { Name = beer.Country.Name },
                Style = new BeerStyleDTO() { Name = beer.Style.Name, Description = beer.Style.Description },
                Brewery = new BreweryDTO() { Name = beer.Brewery.Name, Country = beer.Brewery.Country.Name },
                Reviews = beer.Reviews.Select(r => new ReviewDTO() { }).ToList()
            };

            return beerDTO;
        }

        public BeerDTO Update(int id, BeerDTO beerDTO)
        {
            var beer = _context.Beers.Find(id);
            beer.Name = beerDTO.Name;
            beer.Country = _context.Countries.FirstOrDefault(c => c.Name == beerDTO.Country.Name);
            beer.Style = _context.BeerStyles.Find(beerDTO.Style.ID);
            beer.Brewery = _context.Breweries.Find(beerDTO.Brewery.ID);
            beer.ModifiedOn = DateTime.UtcNow;
            _context.Beers.Update(beer);
            this._context.SaveChangesAsync();

            return beerDTO;
        }
    }
}
