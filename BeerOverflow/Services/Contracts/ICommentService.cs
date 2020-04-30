using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface ICommentService
    {
        Task<CommentDTO> CreateAsync(CommentDTO model);
        Task<bool> DeleteAsync(int? id);
        Task<CommentDTO> GetAsync(int? id);
        Task<IEnumerable<CommentDTO>> GetAllAsync();
        Task<CommentDTO> UpdateAsync(int? id, CommentDTO model);
    }
}
