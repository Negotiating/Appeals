using Appeals.Models;

namespace Appeals.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicDTO>> GetAllAsync();
        Task<Topic> GetByIdAsync(int id);
    }
}
