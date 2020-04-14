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


        public BeerStyleDTO Get(int id)
        {
            var theBeerStyle = _context.BeerStyles
                .Where(style => style.IsDeleted == false)
                .FirstOrDefault(style => style.ID == id);

            if (theBeerStyle == null)
            {
                throw new ArgumentNullException();
            }
            //Todo:convert to dto
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
                //ID = beerStyleDTO.ID, //id to be set by DB itself
                Name = beerStyleDTO.Name,
                Description = beerStyleDTO.Description,
                CreatedOn = DateTime.UtcNow,
            };
            //TODO: check if such style already exists, then do not add it
            this._context.BeerStyles.Add(beerStyle);
            this._context.SaveChanges();

            return beerStyleDTO;
        }
        public async Task<BeerStyleDTO> CreateAsync(BeerStyleDTO beerStyleDTO)
        {
            var beerStyle = new BeerStyle
            {
                Name = beerStyleDTO.Name,
                Description = beerStyleDTO.Description,
                CreatedOn = DateTime.UtcNow,
            };

            await this._context.BeerStyles.AddAsync(beerStyle);
            await this._context.SaveChangesAsync();

            return beerStyleDTO;
        }

        public BeerStyleDTO Update(int id, BeerStyleDTO beerStyleDTO)
        {
            if (id != beerStyleDTO.ID)
            {
                throw new ArgumentNullException();
            }
            //TODO: convert to DTO
            var beerStyle = this._context.BeerStyles.Find(id) ?? throw new ArgumentNullException("The style is not found");

            beerStyle.Description = beerStyleDTO.Description;
            beerStyle.ModifiedOn = DateTime.UtcNow;
            //_context.Entry(beerStyle).State = EntityState.Modified;

            //TODO: call update method, maybe?
            this._context.Update(beerStyle);

            try
            {
                this._context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerStyleExists(id))
                {
                    return beerStyleDTO;
                }
                else
                {
                    throw;
                }
            }

            return beerStyleDTO;
        }
        public async Task<BeerStyleDTO> UpdateAsync(int id, BeerStyleDTO beerStyleDTO)
        {
            if (id != beerStyleDTO.ID)
            {
                throw new ArgumentNullException();
            }
            //TODO: convert to DTO
            var beerStyle = this._context.BeerStyles.Find(id);
            beerStyle.Description = beerStyleDTO.Description;
            beerStyle.ModifiedOn = DateTime.UtcNow;
            //_context.Entry(beerStyle).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerStyleExists(id))
                {
                    return beerStyleDTO;
                }
                else
                {
                    throw;
                }
            }

            return beerStyleDTO;
        }


        public bool Delete(int id)
        {
            //TODO: Instead of .Remove() do isDeleted=false, DeletedOn now and do .Update()
            try
            {
                var beerStyle = this._context.BeerStyles.Find(id) ?? throw new ArgumentNullException("Beer Style not found.");
                beerStyle.IsDeleted = true;
                beerStyle.DeletedOn = DateTime.UtcNow;

                //this._context.BeerStyles.Remove(beerStyle);
                this._context.Update(beerStyle);
                this._context.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            //TODO: Instead of .Remove() do isDeleted=false, DeletedOn now and do .Update()

            try
            {
                var beerStyle = await this._context.BeerStyles.FindAsync(id);

                this._context.BeerStyles.Remove(beerStyle);
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
