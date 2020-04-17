using BeerOverflow.Models;
using Services.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public interface IUsersService
    {
        Task<UserDTO> CreateUser(UserDTO model);
        Task<bool> DeleteUser(int id);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<User> GetUser(int id);
        Task<UserDTO> UpdateUser(int id, UserDTO model);
    }
}