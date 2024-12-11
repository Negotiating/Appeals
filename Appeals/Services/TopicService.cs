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
        public async Task<IEnumerable<TopicDTO>> GetAllAsync()
        {
            var topics =  await _topicRepository.GetAllAsync();
            return topics.Select(topic => new TopicDTO { Id = topic.Id, Name = topic.Name });
        }
        public async Task<Topic> GetByIdAsync(int id)
        {
            return await _topicRepository.GetByIdAsync(id);
        }
    }
}
