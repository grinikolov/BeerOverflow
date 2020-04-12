using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BeerOverflow.Models;
using Database;
using Services.DTOs;

namespace Services
{

    public class BeerStylesService : IBeerStylesService
    {
        private readonly BOContext _context;

        public BeerStylesService(BOContext context)
        {
            this._context = context;
        }


        public IEnumerable<BeerStyleDTO> GetAll()
        {
            var beerStyles = this._context.BeerStyles
                .Where(style => style.IsDeleted == false)
                .Select(style => new BeerStyleDTO
                {
                    ID = style.ID,
                    Name = style.Name,
                    Description = style.Description,
                })
                .ToList();
            return beerStyles;
        }


        public BeerStyleDTO GetBeerStyle(int id)
        {
            var theBeerStyle = _context.BeerStyles
                .Where(style => style.IsDeleted == false)
                .FirstOrDefault(style => style.ID == id);

            if (theBeerStyle == null)
            {
                throw new ArgumentNullException();
            }
            //convert to dto
            var beerStyleDTO = new BeerStyleDTO
            {
                ID = theBeerStyle.ID,
                Name = theBeerStyle.Name,
                Description = theBeerStyle.Description,
            };

            return beerStyleDTO;
        }

        public BeerStyleDTO Create(BeerStyleDTO beerStyleDTO)
        {
            var beerStyle = new BeerStyle
            {
                ID = beerStyleDTO.ID,
                Name = beerStyleDTO.Name,
                Description = beerStyleDTO.Description,
                CreatedOn = DateTime.UtcNow,
            };

            _context.BeerStyles.AddAsync(beerStyle);
            this._context.SaveChangesAsync();

            return beerStyleDTO;
        }

        public BeerStyleDTO Update(int id, BeerStyleDTO beerStyleDTO)
        {
            return beerStyleDTO;
            //if (id != beerStyleDTO.ID)
            //{
            //    throw new ArgumentNullException();
            //}
            //TODO: convert to DTO
            //var beerStyle;
            //_context.Entry(beerStyle).State = EntityState.Modified;

            //try
            //{
            //    this._context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!BeerStyleExists(id))
            //    {
            //        return false;
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            //return false;
        }



        public bool Delete(int id)
        {
            try
            {
                var beerStyle =  _context.BeerStyles.Find(id);

                this._context.BeerStyles.Remove(beerStyle);
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
                var beerStyle = await _context.BeerStyles.FindAsync(id);

                this._context.BeerStyles.Remove(beerStyle);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool BeerStyleExists(int id)
        {
            return _context.BeerStyles.Any(bs => bs.ID == id);
        }
    }
}
