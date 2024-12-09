using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task DeleteAsync(int id);
        Task UpdateAsync(User user);
    }
}
