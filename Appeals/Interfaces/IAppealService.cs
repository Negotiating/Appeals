using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface IAppealService
    {
        Task<IEnumerable<Appeal>> GetAllAsync();
        Task<Appeal> GetByIdAsync(int id);
        Task AddAsync(Appeal appeal);
        Task UpdateAsync(Appeal appeal);
        Task DeleteAsync(int id);
    }
}