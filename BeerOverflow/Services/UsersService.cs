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
                .Select(u => MapUserToDTO(u)).ToListAsync();
            return users;
        }


        public async Task<User> GetUser(int id)
        {
            var user = await this._context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return user;
        }

        public async Task<UserDTO> UpdateUser(int id, UserDTO model)
        {
            // // TODO: DO update
            //if (id != model.ID)
            //{
            //    return BadRequest();
            //}

            //this._context.Entry(model).State = EntityState.Modified;

            //try
            //{
            //    await this._context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!UserExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return model;
        }


        public async Task<UserDTO> CreateUser(UserDTO model)
        {
            UserDTO modelToReturn;
            try
            {
                var theUser = new User
                {
                    //user params here
                };
                this._context.Users.Add(theUser);
                await this._context.SaveChangesAsync();

                modelToReturn = MapUserToDTO(theUser);
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

        private bool UserExists(int id)
        {
            return this._context.Users.Any(e => e.ID == id);
        }

        private UserDTO MapUserToDTO(User u)
        {
            var model = new UserDTO
            {
                ID = u.ID,
                Name = u.Name,
                Password = u.Password,
                //DrankLists = u.DrankLists.Select(dl => new DrankListDTO() { },
                //WishLists = u.WishLists.Select(wl => new WishListDTO() { }),
                ReviewsList = u.ReviewList.Select(r => new ReviewDTO()
                {
                    ID = r.ID,
                    Description = r.Description,
                    Rating = r.Rating,
                }).ToList(),
                CommentsList = u.CommentList.Select(c => new CommentDTO()
                {
                    ID = c.ID,
                    LikesCount = c.LikesCount,
                }).ToList(),
                //FlagList
                //LikesList
            };
            return model;
        }


    }
}

