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
        Task<UserDTO> GetUser(int id);
        //UserDTO UpdateUser(int id, UserDTO model);
        Task<UserDTO> UpdateUserAsync(int id, UserDTO model);
        Task<UserDTO> Drink(int userID, int beerID);
        Task<bool> Wish(int userID, int beerID);
        Task<IEnumerable<BeerDTO>> GetDrankBeers(int userID);
    }
}