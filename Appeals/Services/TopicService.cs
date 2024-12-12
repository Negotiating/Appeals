using Appeals.Interfaces;
using Appeals.Models;

namespace Appeals.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;

        public TopicService(ITopicRepository topicRepository)
        {
            _topicRepository = topicRepository;
        }
        public async Task<IEnumerable<Topic>> GetAllAsync()
        {
            return await _topicRepository.GetAllAsync();
        }
        public async Task<Topic> GetByIdAsync(int id)
        {
            return await _topicRepository.GetByIdAsync(id);
        }
    }
}
