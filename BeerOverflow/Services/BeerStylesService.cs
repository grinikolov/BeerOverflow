using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<BeerStyleDTO>> GetAllAsync()
        {
            var beerStyles = await this._context.BeerStyles
                .Where(style => style.IsDeleted == false)
                .Select(style => new BeerStyleDTO
                {
                    ID = style.ID,
                    Name = style.Name,
                    Description = style.Description,
                }).ToListAsync();
            return beerStyles;
        }


        public async Task<BeerStyleDTO> GetAsync(int id)
        {
            var theBeerStyle = await  _context.BeerStyles
                .Where(style => style.IsDeleted == false)
                .FirstOrDefaultAsync(style => style.ID == id);

            if (theBeerStyle == null)
            {
                return null;
            }
            var beerStyleDTO = new BeerStyleDTO
            {
                ID = theBeerStyle.ID,
                Name = theBeerStyle.Name,
                Description = theBeerStyle.Description,
            };

            return beerStyleDTO;
        }

        public async Task<BeerStyleDTO> CreateAsync(BeerStyleDTO model)
        {
            var beerStyle = new BeerStyle
            {
                Name = model.Name,
                Description = model.Description,
                CreatedOn = DateTime.UtcNow,
            };
            //TODO: check if such style already exists, then do not add it
            var theStyle = this._context.BeerStyles.
                Where(bs => bs.IsDeleted == false)
                .FirstOrDefault(b => b.Name.ToLower() == model.Name.ToLower());
            if (theStyle == null)
            {
                await this._context.BeerStyles.AddAsync(beerStyle);
                await this._context.SaveChangesAsync();
            }

            var returnModel =await this._context.BeerStyles.FirstOrDefaultAsync(b => b.Name == model.Name);
            model.ID = returnModel.ID;
            return model;
        }

        public async Task<BeerStyleDTO> UpdateAsync(int id, BeerStyleDTO model)
        {
            var beerStyle = await this._context.BeerStyles.FindAsync(id);
            if (beerStyle == null)
            {
                return null;
            }
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

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var beerStyle = await this._context.BeerStyles.FindAsync(id)
                    ?? throw new ArgumentNullException("Style not found.");
                beerStyle.IsDeleted = true;
                beerStyle.ModifiedOn = beerStyle.DeletedOn = DateTime.UtcNow;
                this._context.Update(beerStyle);
                await this._context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        private bool BeerStyleExists(int id)
        {
            return this._context.BeerStyles.Any(bs => bs.ID == id);
        }
    }
}
