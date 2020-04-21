using BeerOverflow.Models;
using Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IReviewsService
    {
        Task<ReviewDTO> CreateReview(ReviewDTO model);
        Task<bool> DeleteReview(int id);
        Task<IEnumerable<ReviewDTO>> GetAllReviews();
        Task<ReviewDTO> GetReview(int id);
        Task<ReviewDTO> UpdateReview(int id, ReviewDTO model);
    }
}