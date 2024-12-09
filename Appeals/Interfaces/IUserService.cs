using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(User user);
    }
}
