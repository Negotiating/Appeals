using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task <User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task DeleteAsync(int id);
        Task UpdateAsync(User user);
    }
}
