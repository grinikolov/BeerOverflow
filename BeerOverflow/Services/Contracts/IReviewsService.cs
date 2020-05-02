using Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IReviewsService
    {
        Task<ReviewDTO> CreateAsync(ReviewDTO model);
        Task<bool> DeleteAsync(int? id);
        Task<IEnumerable<ReviewDTO>> GetAllAsync();
        Task<IEnumerable<ReviewDTO>> GetAllByBeerAsync(int id);
        Task<ReviewDTO> GetAsync(int? id);
        Task<ReviewDTO> UpdateAsync(int? id, ReviewDTO model);
    }
}