using BeerOverflow.Models;
using Services.Mappers;

using Database;
using Microsoft.EntityFrameworkCore;
using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UsersService : IUsersService
    {
        private readonly BOContext _context;

        public UsersService(BOContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await this._context.Users
                .Where(u => u.IsDeleted == false)
                .Select(u => u.MapUserToDTO()).ToListAsync();

            return users;
        }


        public async Task<UserDTO> GetUser(int id)
        {
            var theUser = await this._context.Users.FindAsync(id);

            if (theUser == null)
            {
                return null;
            }
            var user = theUser.MapUserToDTO();

            return user;
        }

        public UserDTO UpdateUser(int id, UserDTO model)
        {
            var user = this._context.Users.Find(id);
            user.Name = model.Name;
            user.Password = model.Password;
            user.ModifiedOn = DateTime.UtcNow;

            this._context.Users.Update(user);
            this._context.SaveChanges();
            var toReturn = this._context.Users.Find(id).MapUserToDTO();
            return toReturn;
        }
        public async Task<UserDTO> UpdateUserAsync(int id, UserDTO model)
        {
            var user = await this._context.Users.FindAsync(id);
            user.Name = model.Name;
            user.Password = model.Password;
            user.ModifiedOn = DateTime.UtcNow;

            this._context.Users.Update(user);
            await this._context.SaveChangesAsync();
            var userToReturn = await this._context.Users.FindAsync(id);
            return userToReturn.MapUserToDTO();
        }


        public async Task<UserDTO> CreateUser(UserDTO model)
        {
            UserDTO modelToReturn;
            try
            {
                //TODO: Why not try to find user, if already exists and isDeleted=F

                User theUser = model.MapToUser();
                this._context.Users.Add(theUser);
                await this._context.SaveChangesAsync();

                modelToReturn = theUser.MapUserToDTO();
            }
            catch (Exception)
            {
                throw;
            }

            return modelToReturn;
        }


        public async Task<bool> DeleteUser(int id)
        {
            try
            {

                var user = await this._context.Users.FindAsync(id) ?? throw new ArgumentNullException();
                user.IsDeleted = true;
                user.DeletedOn = DateTime.UtcNow;

                this._context.Users.Update(user);
                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region Drink

        public async Task<UserDTO> Drink(int userID, int beerID)
        {
            var theUser = await this._context.Users
                .Include(u => u.DrankLists)
                    .ThenInclude(dl => dl.Beer)
                    .ThenInclude(b => b.Style)
                .Include(u => u.DrankLists)
                    .ThenInclude(dl => dl.Beer)
                    .ThenInclude(b => b.Brewery)
                    .ThenInclude(b => b.Country)
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.ID == userID);

            var theBeer = await this._context.Beers
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.ID == beerID);

            var theNewDrankList = new DrankList()
            {
                UserID = theUser.ID,
                User = theUser,
                BeerID = theBeer.ID,
                Beer = theBeer,
            };

            //// TODO: Check if already
            if (theUser.DrankLists.Contains(theNewDrankList))
            {
                throw new ArgumentException("Already drank this beer.");
            }

            this._context.DrankLists.Add(theNewDrankList);

            UserDTO modelToReturn;
            try
            {
                await this._context.SaveChangesAsync();

                modelToReturn = theUser.MapUserToDTO();
            }
            catch (Exception)
            {
                return null;
            }

            return modelToReturn;
        }

        public async Task<IEnumerable<BeerDTO>> GetDrankBeers(int userID)
        {
            var theBeers = await this._context.DrankLists
                .Include(dl => dl.Beer)
                    .ThenInclude(b => b.Country)
                .Include(dl => dl.Beer)
                    .ThenInclude(b => b.Brewery)
                .Include(dl => dl.Beer)
                    .ThenInclude(b => b.Style)
                .Include(dl => dl.Beer)
                    .ThenInclude(b => b.Reviews)
                .Where(dl => dl.UserID == userID)
                .Select(dl => dl.Beer).ToListAsync();

            var toReturn = theBeers.Select(b => b.MapBeerToDTO()).ToList();
            return toReturn;
        }
        #endregion
        #region Wish

        public async Task<UserDTO> Wish(int userID, int beerID)
        {
            var theUser = await this._context.Users
                .Where(u => u.IsDeleted == false)
                .Include(u => u.WishLists)
                    .ThenInclude(dl => dl.Beer)
                    .ThenInclude(b => b.Style)
                .Include(u => u.WishLists)
                    .ThenInclude(dl => dl.Beer)
                        .ThenInclude(b => b.Brewery)
                        .ThenInclude(b => b.Country)
                .FirstOrDefaultAsync(u => u.ID == userID);

            var theBeer = await this._context.Beers
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.ID == beerID);

            var theNewWishList = new WishList()
            {
                UserID = theUser.ID,
                User = theUser,
                BeerID = theBeer.ID,
                Beer = theBeer,
            };

            //// TODO: Check if already
            if (theUser.WishLists.Contains(theNewWishList))
            {
                throw new ArgumentException("Already desire this beer.");
            }

            this._context.WishLists.Add(theNewWishList);

            UserDTO modelToReturn;
            try
            {
                await this._context.SaveChangesAsync();
                modelToReturn = theUser.MapUserToDTO();
            }
            catch (Exception)
            {
                return null;
            }

            return modelToReturn;
        }

        public async Task<IEnumerable<BeerDTO>> GetWishBeers(int userID)
        {
            var theBeers = await this._context.WishLists
                .Where(wl => wl.UserID == userID)
                .Include(wl => wl.Beer)
                    .ThenInclude(b => b.Country)
                .Include(wl => wl.Beer)
                    .ThenInclude(b => b.Brewery)
                .Include(wl => wl.Beer)
                    .ThenInclude(b => b.Style)
                .Include(wl => wl.Beer)
                    .ThenInclude(b => b.Reviews)
                .Select(wl => wl.Beer).ToListAsync();

            var toReturn = theBeers.Select(b => b.MapBeerToDTO()).ToList();
            return toReturn;
        }
        #endregion

        // TODO: Beer has no Rating
        public async Task<UserDTO> Rate(int userID, int beerID, int theRating)
        {
            var theUser = await this._context.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.ID == userID);

            var theBeer = await this._context.Beers
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.ID == beerID);

            var theNewRating = new BeerUserRating()
            {
                BeerID = theBeer.ID,
                Beer = theBeer,
                UserID = theUser.ID,
                User = theUser,
                Rating = theRating,
            };

            await this._context.BeerUserRatings.AddAsync(theNewRating);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            //Recalculate beer's rating
            var recalculatedRating = await this._context.BeerUserRatings
                .Where(r => r.BeerID == beerID)
                .Select(r => r.Rating).AverageAsync();

            theBeer.Rating =  recalculatedRating;
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return theUser.MapUserToDTO();
        }
        private bool UserExists(int id)
        {
            return this._context.Users.Any(e => e.ID == id);
        }

    }
}

