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
    public class CommentService: ICommentService
    {
        private readonly BOContext _context;

        public CommentService(BOContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Get all comments on record
        /// </summary>
        /// <returns>Returns a modified list of comments on record</returns>
        public async Task<IEnumerable<CommentDTO>> GetAllAsync()
        {
            var comment = await this._context.Comments
                .Include(c => c.Review)
                .Include(c => c.User)
                .ToListAsync();
            var commentDTO = comment.Select(c => c.MapCommentToDTO()).ToList();
            if (commentDTO.Any(c => c.Description == null))
            {
                return null;
            }
            return commentDTO;
        }

        /// <summary>
        /// Gets a comment by ID
        /// </summary>
        /// <param name="id">Id of comment</param>
        /// <returns>Returns a modified specific comment on record</returns>
        public async Task<CommentDTO> GetAsync(int? id)
        {
            var comment = await this._context.Comments
                                    .Include(c => c.Review)
                                    .Include(c => c.User)
                                    .FirstOrDefaultAsync(c => c.ID == id);

            if (comment == null)
            {
                return null;
            }
            var model = comment.MapCommentToDTO();
            if (model.Description == null)
            {
                return null;
            }
            return model;
        }
        /// <summary>
        /// Creates a comment and writes it to the database.
        /// </summary>
        /// <param name="model">Input CommentDTO object</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<CommentDTO> CreateAsync(CommentDTO model)
        {
            var comment = model.MapDTOToComment();
            comment.Review =await _context.Reviews.FindAsync(model.ReviewID);
            comment.User = await _context.Users.FindAsync(model.UserID);
            comment.Beer = await _context.Beers.FindAsync(comment.Review.BeerID);
            if (comment.Description == null)
            {
                return null;
            }
            #region Check if exists
            var theComment = await this._context.Comments
                .FirstOrDefaultAsync(c => c.ReviewID == comment.ReviewID && c.UserID == comment.UserID);

            if (theComment == null)
            {
                comment.CreatedOn = DateTime.UtcNow;
                await this._context.Comments.AddAsync(comment);
                await this._context.SaveChangesAsync();
            }
            else
            {
                theComment.IsDeleted = false;
                theComment.DeletedOn = null;
                theComment.ModifiedOn = DateTime.UtcNow;
                _context.Comments.Update(theComment);
                await this._context.SaveChangesAsync();
            }
            #endregion
            var returnModel = await this._context.Comments
                .FirstOrDefaultAsync(c => c.ReviewID == model.ReviewID && c.UserID == model.UserID);
            model.ID = returnModel.ID;
            return model;
        }

        /// <summary>
        /// Updates the Comment's Description
        /// </summary>
        /// <param name="id">ID of the Comment to be updated.</param>
        /// <param name="model">Input object with update information.</param>
        /// <returns>Returns the reevaluated input object</returns>
        public async Task<CommentDTO> UpdateAsync(int? id, CommentDTO model)
        {

            var comment = await this._context.Comments.FindAsync(id);
            if (comment == null) return null;
            comment.Description = model.Description;
            comment.ModifiedOn = DateTime.UtcNow;
            model.ID = comment.ID;

            this._context.Update(comment);
            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return null;
                }
            }
            return model;
        }

        /// <summary>
        /// Deletes specified record of comment
        /// </summary>
        /// <param name="id">Id of record</param>
        /// <returns>Bool</returns>
        public async Task<bool> DeleteAsync(int? id)
        {
            try
            {
                var comment = await this._context.Comments
                                    .Include(c => c.Review)
                                    .Include(c => c.User)
                                    .FirstOrDefaultAsync(c => c.ID == id) ?? throw new ArgumentNullException("Comment not found.");
                comment.IsDeleted = true;
                comment.ModifiedOn = comment.DeletedOn = DateTime.UtcNow;

                this._context.Update(comment);

                await this._context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        // for MVC only
        //public async Task<bool> RecoverAsync(int id)
        //{
        //    try
        //    {
        //        var country = await this._context.Countries.Include(b => b.Breweries).FirstAsync(c => c.ID == id)
        //            ?? throw new ArgumentNullException("Country not found.");
        //        country.IsDeleted = false;
        //        country.ModifiedOn = DateTime.UtcNow;
        //        country.DeletedOn = null;
        //        // TODO: Delete 

        //        foreach (var brew in country.Breweries)
        //        {
        //            var newBreweryService = new BreweryServices(this._context);
        //            await newBreweryService.DeleteAsync(brew.ID);
        //        }

        //        this._context.Update(country);

        //        await this._context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch (Exception)
        //    {
        //        return false;
        //    }
        //}

        private bool CommentExists(int? id)
        {
            return this._context.Countries.Any(c => c.ID == id);
        }
    }
}
