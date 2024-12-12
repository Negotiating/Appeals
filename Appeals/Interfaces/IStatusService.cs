using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAllAsync();
        Task<Status> GetByIdAsync(int id);
    }
}
