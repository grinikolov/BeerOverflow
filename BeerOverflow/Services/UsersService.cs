using Services.Mappers;
using BeerOverflow.Models;
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

        //public UserDTO UpdateUser(int id, UserDTO model)
        //{
        //    var user = this._context.Users.Find(id);
        //    user.Name = model.Name;
        //    user.Password = model.Password;
        //    user.ModifiedOn = DateTime.UtcNow;

        //    this._context.Users.Update(user);
        //    this._context.SaveChanges();
        //    var toReturn = this._context.Users.Find(id).MapUserToDTO();
        //    return toReturn;
        //}
        public async Task<UserDTO> UpdateUserAsync(int id, UserDTO model)
        {
            // // TODO: DO update
            var user = await this._context.Users.FindAsync(id);
            user.Name = model.Name;
            user.Password = model.Password;
            user.ModifiedOn = DateTime.UtcNow;

            this._context.Users.Update(user);
            await this._context.SaveChangesAsync();
            var toReturn = await this._context.Users.FindAsync(id);
            return toReturn.MapUserToDTO();
        }


        public async Task<UserDTO> CreateUser(UserDTO model)
        {
            UserDTO modelToReturn;
            try
            {
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

        //private User MapToUser(UserDTO model)
        //{
        //    var theUser = new User()
        //    {
        //        Name = model.Name,
        //        Password = model.Password,
        //        CreatedOn = DateTime.UtcNow,
        //        DrankLists = new List<DrankList>(),
        //        WishLists = new List<WishList>(),
        //        ReviewList = new List<Review>(),
        //        CommentList = new List<Comment>(),
        //        FlagList = new List<Flag>(),
        //        LikesList = new List<Like>(),
        //    };
        //    return theUser;
        //}

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

        public async Task<bool> Drink(int userID, int beerID)
        {
            var theUser = await this._context.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.ID == userID);

            var theBeer = await this._context.Beers
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.ID == beerID);

            //TODO: add beer to user's list then return True
            theUser.DrankLists.Add(new DrankList()
            {
                UserID = userID,
                User = theUser,
                BeerID = beerID,
                Beer = theBeer,
            });

            return true;
        }

        public async Task<IEnumerable<BeerDTO>> GetDrankBeers(int userID)
        {
            var theBeers = await this._context.DrankLists
                .Where(dl => dl.UserID == userID)
                .Select(dl => dl.Beer).ToListAsync();
            var toReturn = theBeers.Select(b => b.MapBeerToDTO()).ToList();
            return toReturn;
        }
        public async Task<ReviewDTO> ReviewABeer(ReviewDTO model)
        {
            var theUser = await this._context.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefaultAsync(u => u.ID == model.UserID);

            var theBeer = await this._context.Beers
                .Where(b => b.IsDeleted == false)
                .FirstOrDefaultAsync(b => b.ID == model.BeerID);

            var toReturn = new ReviewDTO();
            return toReturn;
        }



        private bool UserExists(int id)
        {
            return this._context.Users.Any(e => e.ID == id);
        }

        public Task<bool> Wish(int userID, int beerID)
        {
            throw new NotImplementedException();
        }
    }
}

