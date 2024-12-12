using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>> GetAllAsync();
        Task<Topic> GetByIdAsync(int id);
    }
}
